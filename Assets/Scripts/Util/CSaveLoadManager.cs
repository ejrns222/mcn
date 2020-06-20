﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;



public class CSaveLoadManager : MonoBehaviour
{
    private class Wrapper<T>
    {
        public T[] Items;
    }
    
    private const string Key = "ejrns222";

    private void Awake()
    {
    }

    private void OnApplicationQuit()
    {
        CDictionary.Save();
        CSelfCare.Save();
        CDimensionResearch.Save();
        Player.Instance.Save();
        CRecruit.Save();
        CMonitoring.Save();
    }

    private static Wrapper<T> Wrapping<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return wrapper;
    }

    /// <summary>
    /// @brief : 제이슨 파일로 세이브
    /// </summary>
    /// <param name="obj"></param> 저장하고 싶은 객체, 만약 MonoBehavior를 상속받는다면 GetComponent로 가져온뒤 넣어야 한다.
    /// <param name="createPath"></param> 기본적으로 asset으로 경로가 잡혀 있으니 뒷부분 경로만 적어주자
    /// <param name="fileName"></param> 파일이름
    public static void CreateJsonFile(object obj, string createPath, string fileName)
    {
        string jsonData = Encrypt(JsonUtility.ToJson(obj));
        Debug.Log("저장 내용 " + JsonUtility.ToJson(obj));
        FileStream fileStream = new FileStream(Application.dataPath + $"/{createPath}/{fileName}.json", FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    /// <summary>
    /// @brief : 배열을 제이슨 파일로 세이브 (래퍼 클래스에 넣어서 저장함)
    /// </summary>
    public static void CreateJsonFileForArray<T>(T[] array, string createPath, string fileName)
    {
        var obj = Wrapping(array);
        string test = JsonUtility.ToJson(obj);
        string jsonData = Encrypt(JsonUtility.ToJson(obj));
        Debug.Log("배열 저장 내용 " + JsonUtility.ToJson(obj));
        FileStream fileStream = new FileStream(Application.dataPath + $"/{createPath}/{fileName}.json", FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }
    
    /// <summary>
    /// @brief : 제이슨 파일을 로드, MonoBehavior를 상속받던 클래스는 로드 할 수 없음
    ///          클래스만 로드 가능함
    /// </summary>
    public static T LoadJsonFile<T>(string loadPath, string fileName) where T : class
    {
        if (!File.Exists(Application.dataPath + $"/{loadPath}/{fileName}.json"))
            return null;
        FileStream fileStream = new FileStream(Application.dataPath +$"/{loadPath}/{fileName}.json", FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<T>(Decrypt(jsonData));
    }

    /// <summary>
    /// @brief : MonoBehavior를 상속받던 클래스를 로드
    /// </summary>
    /// <param name="component"></param> 로드를 받을 컴포넌트
    /// <param name="loadPath"></param> 경로
    /// <param name="fileName"></param> 이름
    /// <typeparam name="T"></typeparam> 컴포넌트
    public static void LoadJsonFileToGameObject<T>(T component, string loadPath, string fileName) where T : Component
    {
        if (!File.Exists(Application.dataPath + $"/{loadPath}/{fileName}.json"))
            return;
        FileStream fileStream = new FileStream(Application.dataPath +$"/{loadPath}/{fileName}.json", FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        
        GameObject tmpObj = new GameObject();
        var tmpComp = tmpObj.AddComponent<T>();
        JsonUtility.FromJsonOverwrite(Decrypt(jsonData),component);
    }

    /// <summary>
    /// @brief : 배열로 저장한 제이슨 파일을 로드
    /// </summary>
    /// <returns></returns>
    public static T[] LoadJsonFileToArray<T>(string loadPath, string fileName)
    {
        if (!File.Exists(Application.dataPath + $"/{loadPath}/{fileName}.json"))
            return null;
        FileStream fileStream = new FileStream(Application.dataPath +$"/{loadPath}/{fileName}.json", FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        var wrappedClass = JsonUtility.FromJson<Wrapper<T>>(Decrypt(jsonData));
        return wrappedClass.Items;
    }
    
    //암호화
    private static string Encrypt(string textToEncrypt)
    {

        RijndaelManaged rijndaelCipher = new RijndaelManaged();
       
        rijndaelCipher.Mode = CipherMode.CBC;
        rijndaelCipher.Padding = PaddingMode.PKCS7;
        rijndaelCipher.KeySize = 128;
        rijndaelCipher.BlockSize = 128;

        byte[] pwdBytes = Encoding.UTF8.GetBytes(Key);
        byte[] keyBytes = new byte[16];

        int len = pwdBytes.Length;
        if (len > keyBytes.Length)
        {
            len = keyBytes.Length;
        }

        Array.Copy(pwdBytes, keyBytes, len);

        rijndaelCipher.Key = keyBytes;
        rijndaelCipher.IV = keyBytes;

        ICryptoTransform encrypt = rijndaelCipher.CreateEncryptor();
        byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);

        return Convert.ToBase64String(encrypt.TransformFinalBlock(plainText, 0, plainText.Length));
    }
    
    //복호화
    private static string Decrypt(string textToDecrypt)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();

        rijndaelCipher.Mode = CipherMode.CBC;
        rijndaelCipher.Padding = PaddingMode.PKCS7;
        rijndaelCipher.KeySize = 128;
        rijndaelCipher.BlockSize = 128;

        byte[] encryptedData = Convert.FromBase64String(textToDecrypt);
        byte[] pwdBytes = Encoding.UTF8.GetBytes(Key);
        byte[] keyBytes = new byte[16];
        int len = pwdBytes.Length;

        if (len > keyBytes.Length)
        {
            len = keyBytes.Length;
        }

        Array.Copy(pwdBytes, keyBytes, len);

        rijndaelCipher.Key = keyBytes;
        rijndaelCipher.IV = keyBytes;

        byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);

        return Encoding.UTF8.GetString(plainText);

    }

}
