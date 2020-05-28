using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Util;
using Wealths;
using Random = UnityEngine.Random;

//골드 수급하는 컨텐츠
public class CVideoEdit : MonoBehaviour
{
    private Slider _slider;
    private uint _numEdit = 0;
    private List<CEditButton> _editButtons = new List<CEditButton>();
    public CEditButton editButtonPrefab;

    void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
        CTimeManager.Instance.onOneDayElapse += OnEditEnd;
        CTimeManager.Instance.onOneYearElapse += OnYearEnd;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        foreach (var v in Player.Instance.equippedStreamers)
        {
            GameObject tmp;
            tmp = Instantiate(editButtonPrefab.gameObject, GameObject.Find("EditButtons").transform);
            CEditButton tmpEditButton = tmp.GetComponent<CEditButton>();
            IStreamer currentStreamer = v.GetComponent<IStreamer>();
            tmpEditButton.price = currentStreamer.AdPrice;
            tmpEditButton.buttonText = tmp.GetComponentInChildren<Text>();
            tmpEditButton.buttonText.text = currentStreamer.Tag.ToString();
            tmpEditButton.streamer = v;
        }
    }

    void Update()
    {
        _slider.value = CTimeManager.Instance.DeltaTimeForDay / CTimeManager.Instance.OneDay;
    }

    /// <summary>
    /// @brief : 게임 내 매 일 편집이 끝났을 때 호출 될 함수 타임매니저가 호출함
    ///          골드 계산식 : 기본금 + 스킬에 의한 증가량
    /// </summary>
    void OnEditEnd()
    {
        _numEdit++;
        BigInteger calculatedGold = CalculateGold();
        Gold.Instance.Value += calculatedGold; 
        GameObject.Find("Gold").GetComponent<CWealthRenderer>().RenderEarnedWealth(calculatedGold);

        int idx = Random.Range(0, Player.Instance.equippedStreamers.Count);
        Player.Instance.equippedStreamers[idx].GetComponent<IStreamer>().Subscribers++;

        foreach (var v in Player.Instance.equippedStreamers)
        {
            var tmp = v.GetComponent<IStreamer>();
            tmp.Subscribers += (uint)((float)tmp.IncreaseSubs * ((float)tmp.AdLevel / 2f));
        }
    }

    void OnYearEnd()
    {
        BigInteger salary = CalculateSalary();
        Gold.Instance.Value += salary;
    }

    public BigInteger CalculateGold()
    {
        BigInteger calculatedGold = Player.Instance.editPay;
       
        ///////////////
        //스킬 계산할 곳
        ///////////////

        return calculatedGold;
    }

    public BigInteger CalculateSalary()
    {
        return Player.Instance.basicSalary + (Player.Instance.basicSalary / 100 * _numEdit);
    }
}
