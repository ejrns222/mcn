using System;
using UnityEngine;
using UnityEngine.UI;

namespace Wealths
{
    public enum EWealth
    {
        Mileage,Gold,Jewel   
    }

    /// <summary>
    /// @brief : 재화와 재화의 증가값을 출력한다.
    /// </summary>
    public class CWealthRenderer : MonoBehaviour
    {
        private static GameObject _earnedWealthPrefab;

        private static GameObject _mileage, _gold, _jewel;
    
        private void Awake()
        {
           _earnedWealthPrefab = Resources.Load("UIPrefabs/EarnedWealth") as GameObject;
            _mileage = transform.GetChild(0).gameObject;
            _gold = transform.GetChild(1).gameObject;
            _jewel = transform.GetChild(2).gameObject;
        }

        private void FixedUpdate()
        {
            _mileage.transform.Find("Text").GetComponent<Text>().text =
                UnitConversion.ConverseUnit(Player.Instance.mileage).ConversedUnitToString();
            _gold.transform.Find("Text").GetComponent<Text>().text = 
                UnitConversion.ConverseUnit(Player.Instance.gold).ConversedUnitToString();
            _jewel.transform.Find("Text").GetComponent<Text>().text = 
                UnitConversion.ConverseUnit(Player.Instance.jewel).ConversedUnitToString();
        }
    
        //TODO : 열거형 없애기
        public static void RenderEarnedWealth(long value, EWealth eWealth)
        {
            if (_earnedWealthPrefab != null)
            {
                var temp = _earnedWealthPrefab.GetComponent<CEarnedWealthRenderer>();
                GameObject instanceObj = Instantiate(_earnedWealthPrefab);
           
                switch (eWealth)
                {
                    case EWealth.Mileage:
                        instanceObj.transform.SetParent(_mileage.transform);
                        break;
                    case EWealth.Gold:
                        instanceObj.transform.SetParent(_gold.transform);
                        break;
                    case EWealth.Jewel:
                        instanceObj.transform.SetParent(_jewel.transform);
                        break;
                }

                instanceObj.GetComponent<CEarnedWealthRenderer>().value = value;
                instanceObj.transform.localPosition = new Vector3(25f, -20f, 0);
            }
        }
    }
}