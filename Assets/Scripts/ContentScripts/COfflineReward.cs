using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;
using Wealths;

public class COfflineReward : MonoBehaviour
{
    private Text _touch;
    private Text _reward;
    private static bool _isOnce = false; 

    private void Awake()
    {
        if (_isOnce)
        {
            gameObject.SetActive(false);
            return;
        }

        var isTutorialEnd =CSaveLoadManager.LoadJsonFileToArray<bool>("SaveFiles", "TutorialEnd2");
        if(isTutorialEnd == null)
            gameObject.SetActive(false);
        
        _touch = transform.Find("Mail").Find("Panel").Find("Touch").GetComponent<Text>();
        _reward = transform.Find("MailBox").Find("Panel").Find("Reward").GetComponent<Text>();
        _isOnce = true;
    }

    private void Start()
    {
        StartCoroutine(FadeInOut());
        _reward.text =
            $"급여 : {UnitConversion.ConverseUnit(CTimeManager.Instance.todayOfflineSalary).ConversedUnitToString()} " +
            $"\n편집수당 : {UnitConversion.ConverseUnit(CTimeManager.Instance.todayOfflineGold).ConversedUnitToString()} " +
            $"\n마일리지 : {UnitConversion.ConverseUnit(CTimeManager.Instance.todayOfflineMileage).ConversedUnitToString()}";
    }

    IEnumerator FadeInOut()
    {
         
        bool isFadeOut = true;
        while (true)
        {
            var textColor = _touch.color;
            if (isFadeOut)
            {
                textColor.a -= 0.05f;
                if (textColor.a < 0.1f)
                {
                    isFadeOut = false;
                }
            }
            else
            {
                textColor.a += 0.05f;
                if (textColor.a > 0.9f)
                {
                    isFadeOut = true;
                }
            }
            _touch.color = textColor;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
