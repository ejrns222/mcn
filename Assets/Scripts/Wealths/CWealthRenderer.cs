using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Wealths;
using Vector3 = UnityEngine.Vector3;

public enum EWealth
{
    Mileage,
    Jewel,
    Money,
}
public class CWealthRenderer : MonoBehaviour
{
    public EWealth selectedWealth;
    private Text _text;
    private IWealth _wealth;
    public GameObject earnedWealthPrefab;

    private void Awake()
    {
        //_text = gameObject.GetComponent<Text>();
        _text = GetComponentInChildren<Text>();
        
        switch (selectedWealth)
        {
            case EWealth.Jewel:
                _wealth = Jewel.Instance;
                break;
            case EWealth.Mileage:
                _wealth = Mileage.Instance;
                break;
            case EWealth.Money:
                _wealth = Gold.Instance;
                break;
        }
    }

    private void FixedUpdate()
    {
        _text.text = _wealth.ConversedUnit();
    }
    
    public void RenderEarnedWealth(BigInteger value)
    {
        if (earnedWealthPrefab != null)
        {
            var temp = earnedWealthPrefab.GetComponent<CEarnedWealthRenderer>();

            GameObject instanceObj = Instantiate(earnedWealthPrefab, transform, true);
            instanceObj.GetComponent<CEarnedWealthRenderer>().value = value;
            instanceObj.transform.localPosition = new Vector3(25f,20f,0);
        }
    }
}
