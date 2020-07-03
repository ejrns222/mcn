using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMovingManager : MonoBehaviour
{
    [SerializeField] private GameObject tablet;
    [SerializeField] private GameObject room;
    [SerializeField] private GameObject backGround;

    private void Awake()
    {
        Screen.SetResolution(720,1280,true);
        iTween.MoveTo(tablet, iTween.Hash("y", -190,"isLocal",true, "easeType", "easeOutExpo","delay",2f,"Time",1.5f));
        iTween.MoveTo(room, iTween.Hash("y", 0, "easeType", "easeOutSine","Time",1f,"delay",0.5f));
        iTween.MoveTo(backGround, iTween.Hash("y", 7, "easeType", "easeOutSine","Time",1f,"delay",0.5f));
    }
}
