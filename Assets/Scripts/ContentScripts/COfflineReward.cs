using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class COfflineReward : MonoBehaviour
{
    private Text _touch;
    private Text _reward;

    private void Awake()
    {
        _touch = transform.Find("Mail").Find("Panel").Find("Touch").GetComponent<Text>();
        _reward = transform.Find("MailBox").Find("Panel").Find("Reward").GetComponent<Text>();
    }

    private void Start()
    {
        StartCoroutine(FadeInOut());
        _reward.text =
            $"급여 : {CTimeManager.Instance.todayOfflineSalary} \n편집수당 : {CTimeManager.Instance.todayOfflineGold} \n마일리지 : {CTimeManager.Instance.todayOfflineMileage}";
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
