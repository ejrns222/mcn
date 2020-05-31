using System;
using System.Collections;
using System.Collections.Generic;
using Characters;
using Characters.Streamers;
using UnityEngine;
using UnityEngine.Serialization;

public class CInventory : MonoBehaviour
{
    public static CInventory Instance = null;
    public List<IStreamer> streamerList = new List<IStreamer>();

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else if(Instance != this)
            Destroy(gameObject);
        
        //테스트용
        streamerList.Add(new CTestTaek());
        streamerList.Add(new CTestHun());
        streamerList.Add(new CTestHyun());
        
        gameObject.SetActive(false);
    }
}
