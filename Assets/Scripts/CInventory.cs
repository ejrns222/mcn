using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Characters;
using Characters.Streamers;
//using Characters.Streamers.RankD;
using UnityEngine;
using UnityEngine.UI;

public class CInventory : MonoBehaviour
{
    public static CInventory Instance = null;
    public List<StreamerBase> streamerList = new List<StreamerBase>();
    

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else if(Instance != this)
            Destroy(gameObject);
        
        Load();
        /*//테스트용
        streamerList.Add(new CTestTaek());
        streamerList.Add(new CTestHyun());
        streamerList.Add(new CTestHun());
        streamerList.Add(new CGun());*/
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Save()
    {
        List<string> saveStreamer = new List<string>();
        foreach (var v in streamerList)
        {
            saveStreamer.Add(StreamerBaseForJson.StreamerToString(v));
        }
        CSaveLoadManager.CreateJsonFileForArray(saveStreamer.ToArray(),"SaveFiles","Inventory");
    }

    private void Load()
    {
        var streamers = CSaveLoadManager.LoadJsonFileToArray<string>("SaveFiles","Inventory");
        if(streamers == null)
            return;
        
        foreach (var v in streamers)
        {
            streamerList.Add(StreamerBaseForJson.StringToStreamer(v));
        }
    }
}
