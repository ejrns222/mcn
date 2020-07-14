using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Util;

public class CDimensionDoor : MonoBehaviour
{
    private int numEntry = 0;
    public Text door1Text;
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Save()
    {
        CSaveLoadManager.CreateJsonFile(numEntry,"SaveFiles","DimensionDoor");
    }

    void Load()
    {
        if (CTimeManager.LastRealTime.Date != DateTime.Today)
        {
            numEntry = 0;
        }
    }

    public void OnDoor1Click()
    {
        if (numEntry >= 3)
        {
            var dm = CDialogManager.CreateDialog("Me:지금 가기엔 너무 위험해");
        }
        else
        {
            var dm = CDialogManager.CreateDialog("Me:자... 가볼까?");
            dm.AddOnDialogEnd(LoadSceneMode);
            dm.AddOnDialogEnd(GameObject.Find("MovingManager").GetComponent<CMovingManager>().ExitOffice);
            numEntry++;
            door1Text.text = $"{3 - numEntry}/3";
        }
    }

    private void LoadSceneMode()
    {
        Debug.Log("이동하자");
    }
}
