using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Wealths;

public class Player : MonoBehaviour
{
   public uint Jewel { get; set; } //유료재화
   public UInt64 Money { get; set; } //플레이어의 강화에 사용되는 재화

   delegate void MileageCalcDelegate();

   
   
   private static Player instance;
   
   ////////////////////////TEST/////////////////////////////
   
   public List<Streamer> _streamers = new List<Streamer>();
   
   /// //////////////////////////////////////////////////////
  
   
   
   public static Player Instance
   {
      get
      {
         if (instance == null)
         {
            var obj = FindObjectOfType<Player>();
            if (obj != null)
            {
               instance = obj;
            }
            else
            {
               var newSinglton = new GameObject("Player").AddComponent<Player>();

               instance = newSinglton;
            }
         }

         return instance;
      }
      private set
      {
         instance = value;
      }
   }

   private void Awake()
   {
      var objs = FindObjectsOfType<Player>();
      if (objs.Length != 1)
      {
         Destroy(gameObject);
         return;
      }
      DontDestroyOnLoad(gameObject);

     // Mileage = 0;
      Jewel = 0;
      Money = 0;
      ////////////////////////TEST/////////////////////////////
      _streamers.Add(Streamer.MakeStreamer(EStreamer.TestHun));
      _streamers.Add(Streamer.MakeStreamer(EStreamer.TestHyun));
      foreach (var v in _streamers)
      {
         v.TestLog();
      }

      StartCoroutine("IncreaseMileage");
   }

   public bool FindStreamer(EStreamer streamerName)
   {
      foreach (var v in _streamers)
      {
         if (v.Tag == streamerName)
            return true;
      }

      return false;
   }

   IEnumerator IncreaseMileage()
   {
      
      while (true)
      {
         uint factor = 100;
         foreach (var v in _streamers)
         {
            factor += v.Skill();
         }
         CMileage.Instance.Value += factor;
         yield return new WaitForSeconds(1f);
      }
   }
}

