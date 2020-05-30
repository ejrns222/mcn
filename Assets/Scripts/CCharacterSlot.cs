using System;
using System.Collections;
using System.Collections.Generic;
using Characters;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Util;

public class CCharacterSlot : MonoBehaviour
{
    public IStreamer streamer;
    private Text _streamerName;

    private void Start()
    {
        _streamerName = GetComponentInChildren<Text>();
        _streamerName.text = streamer.Tag.ToString();
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);  
    }

    private void OnClick()
    {
        GameObject.Find("CharacterChangeWindow").GetComponent<CCharacterChanger>().CharacterChange(gameObject);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
        
    }
}
