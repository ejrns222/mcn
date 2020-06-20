using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;
using Wealths;

//골드 수급하는 컨텐츠
public class CVideoEdit : MonoBehaviour
{
    private Slider _slider;
    private uint _numEdit = 0;
    private List<CEditSlot> _editSlots = new List<CEditSlot>();
    
    public CEditSlot contentSlotPrefab;

    void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
    }

    private void Start()
    {
        CTimeManager.Instance.onOneDayElapse += OnEditEnd;
        CTimeManager.Instance.onOneYearElapse += OnYearEnd;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        foreach (var v in Player.Instance.equippedStreamers)
        {
            if(v == null)
                continue;
            
            GameObject slot = Instantiate(contentSlotPrefab.gameObject, GameObject.Find("EditButtons").transform);
            slot.GetComponent<CEditSlot>().streamer = v;
            _editSlots.Add(slot.GetComponent<CEditSlot>());

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
        long calculatedGold = CalculateGold();
        Player.Instance.gold += calculatedGold; 
        CWealthRenderer.RenderEarnedWealth(calculatedGold,EWealth.Gold);

        foreach (var v in Player.Instance.equippedStreamers)
        {
            if(v == null)
                continue;
            if(v.Subscribers < v.Expectation)
                v.Subscribers += (v.IncreasingSubs * ((uint) (v.AdLevel / 20f) + 1) * (v.AdLevel - 20 * (uint) (v.AdLevel / 20f) / 2));
        }
    }

    //연봉
    void OnYearEnd()
    {
        long salary = CalculateSalary();
        Player.Instance.gold += salary;
    }

    public long CalculateGold()
    {
        long calculatedGold = Player.Instance.EditPay;
        ///////////////
        //스킬 계산할 곳
        ///////////////
        uint lv = CSelfCare.CareSkill.EditLevel;
        double multiple = 1d + (10 * ((uint) (lv / 50f) + 1) * (lv - 50 * (uint) (lv / 50f) / 2)) / 100d; //10퍼씩 증가, 50레벨당 10퍼씩 추가 증가
        calculatedGold = (long)Math.Round(multiple * calculatedGold);
        
        return calculatedGold;
    }

    public long CalculateSalary()
    {
        long calculatedSalary = Player.Instance.BasicSalary;
        uint lv = CSelfCare.CareSkill.ComputerLevel;
        calculatedSalary= (long)Math.Round(calculatedSalary * (1d+ (lv * _numEdit) / 100d));
        return calculatedSalary;
    }
}
