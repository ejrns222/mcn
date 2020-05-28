using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CInventory : MonoBehaviour
{
    public static CInventory Instance = null;
    public List<GameObject> streamerList = new List<GameObject>();

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else if(Instance != this)
            Destroy(gameObject);
        
        //테스트용
        streamerList.Add(Resources.Load("CharacterPrefabs/TestTaek") as GameObject);
        streamerList.Add(Resources.Load("CharacterPrefabs/TestTaek") as GameObject);
        streamerList.Add(Resources.Load("CharacterPrefabs/TestTaek") as GameObject);
        
        gameObject.SetActive(false);
    }
}
