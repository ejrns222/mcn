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
public class CWealthRenderer : MonoBehaviour
{
    public EWealth selectedWealth;
    private Text _text;
    private IWealth _wealth;
    public GameObject earnedWealthPrefab;

    private void Awake()
    {
        _text = gameObject.GetComponent<Text>();
        
        switch (selectedWealth)
        {
            case EWealth.Jewel:
                _wealth = Jewel.Instance;
                break;
            case EWealth.Mileage:
                _wealth = Mileage.Instance;
                break;
            case EWealth.Money:
                _wealth = Money.Instance;
                break;
        }
    }

    private void FixedUpdate()
    {
        _text.text = _wealth.ConversedUnit();
    }
    
    public void RenderEarnedWealth(uint value)
    {
        if (earnedWealthPrefab != null)
        {
           // Vector3 tempVector = gameObject.transform.position;
            //tempVector.y = tempVector.y + 5;
            var temp = earnedWealthPrefab.GetComponent<CEarnedWealthRenderer>();
            temp.value = value;

            GameObject instanceObj = Instantiate(earnedWealthPrefab, transform, true);
            instanceObj.transform.localPosition = new Vector3(167,266,0);
        }
    }
}
