﻿using System;
using Characters;
using UnityEngine;
using UnityEngine.UI;

public class CCharacterChangeSlot : MonoBehaviour
{
    public StreamerBase streamer;
    public Image image;
    public CCharacterChanger characterChanger;
    private Text _streamerName;

    private void Start()
    {
        image = GetComponent<Image>();
        _streamerName = GetComponentInChildren<Text>();
        _streamerName.text = streamer.Tag.ToString();
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);  
    }

    private void OnClick()
    {
        //characterChanger.CharacterChange(this);
        characterChanger.SelectSlot(this);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
        
    }
}
