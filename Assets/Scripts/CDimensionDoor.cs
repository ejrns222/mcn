using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Util;

public class CDimensionDoor : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void OnDoor1Click()
    { 
        var isTutorialEnd =CSaveLoadManager.LoadJsonFileToArray<bool>("SaveFiles", "TutorialEnd2");
        if (isTutorialEnd == null)
        {
            GameObject.Find("MovingManager").GetComponent<CMovingManager>().ExitOffice();
            return;
        }
        var dm = CDialogManager.CreateDialog("Me:자... 가볼까?");
        dm.AddOnDialogEnd(GameObject.Find("MovingManager").GetComponent<CMovingManager>().ExitOffice);
    }
}
