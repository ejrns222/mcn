using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wealths;

public enum EWealth
{
    Mileage,
    Jewel,
    Money,
}
public class WealthToText : MonoBehaviour
{
    public EWealth SelectedWealth;
    private Text _text;

    private void Awake()
    {
        _text = gameObject.GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        switch (SelectedWealth)
        {
            case EWealth.Jewel:
                _text.text = Player.Instance.Jewel.ToString();
                break;
            case EWealth.Mileage:
                _text.text = CMileage.Instance.ConversedMileage();
                break;
            case EWealth.Money:
                _text.text = Player.Instance.Money.ToString();
                break;
        }
        
    }
}
