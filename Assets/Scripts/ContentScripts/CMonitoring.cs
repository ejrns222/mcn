using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Wealths;
using Characters;
using UIScripts;
using Util;
using Random = UnityEngine.Random;

[SuppressMessage("ReSharper", "PossibleLossOfFraction")]
public class CMonitoring : MonoBehaviour
{
    private static CEquipSlot[] _equipSlots;

    private void Awake()
    {
        _equipSlots = GetComponentsInChildren<CEquipSlot>();
        Load();
    }

    private void Start()
    {
        CTimeManager.Instance.onOneHourElapse += OnIncreaseMileage;
        Refresh();
    }

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        foreach (var v in _equipSlots)
        {
            v.Refresh();
        }
    }
    
        
    private void OnIncreaseMileage()
    {
        var calculatedMileage = CalculateMileage(true);
        Player.Instance.mileage += calculatedMileage;
        CWealthRenderer.RenderEarnedWealth(calculatedMileage,EWealth.Mileage);

        //자기관리 스킬에 따른 모니터링중 구독자 수 증가 계산
        if(CSelfCare.CareSkill.BroadCastLevel > 0)
            for (int i = 0; i < Player.Instance.equippedStreamers.Length; i++)
            {
                if(Player.Instance.equippedStreamers[i] == null)
                    continue;
                if(Player.Instance.equippedStreamers[i].Expectation >= Player.Instance.equippedStreamers[i].Subscribers + CSelfCare.CareSkill.BroadCastLevel)
                    Player.Instance.equippedStreamers[i].Subscribers+= CSelfCare.CareSkill.BroadCastLevel;
            }
    }

    /// <summary>
    /// 획득 마일리지 계산
    /// 각 스트리머의 구독자*등급보정치 의 합 + 각 스트리머의 스킬에 의한 증가치 *(건물주나 사장님 출현시 배수)
    /// <param name="isOnline">false면 크리티컬 적용되지 않는다.</param>
    /// </summary>
    public long CalculateMileage(bool isOnline)
    {
        long calculatedMileage = 0;
        foreach (var v in Player.Instance.equippedStreamers)
        {
            if(v == null)
                continue;
            
            calculatedMileage += (uint)((v.Subscribers/10) * LevelCorrection(v.Rank));
        }

        long skilledMileage = 0;
        foreach (var v in Player.Instance.equippedStreamers)
        {
            //
            if(v == null)
                continue;
            skilledMileage += v.Skill(calculatedMileage);
        }
        calculatedMileage += skilledMileage;

        
        //크리티컬 계산
        if (calculatedMileage > 0 && isOnline)
        {
            float critical = Random.Range(0f, 1f);
            if (critical <= Player.Instance.ProbBoss)
                calculatedMileage *= 100;
            if (Player.Instance.ProbBoss < critical && critical <= Player.Instance.ProbBoss + Player.Instance.probLord)
                calculatedMileage *= 1000;
        }

        //챗봇업글에따른 증가치
        {
            uint lv = CSelfCare.CareSkill.BotLevel;
            double multiple = 1d + (10 * ((uint) (lv / 50f) + 1) * (lv - 50 * (uint) (lv / 50f) / 2)) / 100d; //최초 10이지만 레벨이 50배수 일때 상승량이 10씩 오른다.
            calculatedMileage= (long)Math.Round(multiple * calculatedMileage);
        }
        return calculatedMileage;
    }
    
    private static float LevelCorrection(ERank rank)
    {
        switch (rank)
        {
            case ERank.A:
                return 1.5f;
            case ERank.B:
                return 1.3f;
            case ERank.C:
                return 1.2f;
            case ERank.D:
                return 1.1f;
            case ERank.E:
                return 1.05f;
            case ERank.F:
                return 1.0f;
        }
        return 0.0f;
    }

    public static void Save()
    {
        int[] slotState = new int[_equipSlots.Length];
        for (int i = 0; i < _equipSlots.Length; i++)
        {
            slotState[i] = (int)_equipSlots[i].slotState;
        }
        CSaveLoadManager.CreateJsonFileForArray(slotState,"SaveFiles","Monitoring");
    }

    private void Load()
    {
        int[] slotState = CSaveLoadManager.LoadJsonFileToArray<int>("SaveFiles", "Monitoring");
        if (slotState == null)
        {
            for (int i = 0; i < Player.Instance.maxNumStreamers; i++)
            {
                _equipSlots[i].ChangeState(SlotState.OpenEmpty);
            }
            _equipSlots[Player.Instance.maxNumStreamers].ChangeState(SlotState.Locked);
            for (int i = (int) Player.Instance.maxNumStreamers + 1; i < _equipSlots.Length; i++)
            {
                _equipSlots[i].ChangeState(SlotState.Disabled);
            }
            return;
        }
        for (int i = 0; i < _equipSlots.Length; i++)
        {
            _equipSlots[i].ChangeState((SlotState)slotState[i]);
        }
    }
}
