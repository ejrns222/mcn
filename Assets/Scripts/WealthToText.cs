using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
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
    public EWealth selectedWealth;
    private Text _text;

    private void Awake()
    {
        _text = gameObject.GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        switch (selectedWealth)
        {
            case EWealth.Jewel:
                _text.text = Jewel.Instance.ConversedJewel();
                break;
            case EWealth.Mileage:
                _text.text = Mileage.Instance.ConversedMileage();
                break;
            case EWealth.Money:
                _text.text = Money.Instance.ConversedMoney();
                break;
        }
        
    }
}
