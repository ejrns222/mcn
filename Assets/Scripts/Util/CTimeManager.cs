using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;
using Wealths;

/// <summary>
/// @brief : 싱글톤, 게임상 1시간 혹은 1일마다 호출되는 델리게이트를 가짐
/// </summary>
public class CTimeManager : MonoBehaviour
{
    //저장용 클래스
    private class SavedTime
    {
        public float DeltaTimeForHour;
        public float DeltaTimeForDay;
        public float DeltaTimeForYear;
        public DateTime LastTime;
    }
    
    public static CTimeManager Instance = null;
    private DateTime _currentTime;
    private DateTime _lastTime; //마지막에 게임을 종료했을 때의 시간을 저장
    public float timeFactor = 0f; //클수록 시간이 빠르게 흐른다.
    
    private const float _oneHour = 10f;
    private const float _oneDay = 240f;
    private const float _oneYear = 86400f;


    private float _deltaTimeForHour;//저장
    private float _deltaTimeForDay;//저장
    private float _deltaTimeForYear;//저장

    public float DeltaTimeForHour => _deltaTimeForHour;
    public float DeltaTimeForDay => _deltaTimeForDay;
    public float DeltaTimeForYear => _deltaTimeForYear;

    public float OneHour => _oneHour* (1f - timeFactor);
    public float OneDay => _oneDay * (1f - timeFactor);
    public float OneYear => _oneYear * (1f - timeFactor);
    
    public delegate void OneTimeHandler();

    public OneTimeHandler onOneDayElapse;
    public OneTimeHandler onOneHourElapse;
    public OneTimeHandler onOneYearElapse;

    void Awake()
    {
        if (!Instance)
            Instance = this;
        else if(Instance != this)
            Destroy(gameObject);

        _currentTime = DateTime.Now;
        _deltaTimeForHour = 0;
        _deltaTimeForDay = 0;
        _deltaTimeForYear = 500;
        LoadGameTime();
        
        Debug.Log(_currentTime);
        
        ///////TEST///////
        _lastTime = new DateTime(2020,5,27,23,20,35);
        
        OfflineReward();
    }

    private void Update()
    {
        _deltaTimeForHour += Time.deltaTime;
        _deltaTimeForDay += Time.deltaTime;
        _deltaTimeForYear += Time.deltaTime;
        
        if (_deltaTimeForHour >= OneHour)
        { 
            Debug.Log("OnOneGameHour");
            onOneHourElapse?.Invoke();
            _deltaTimeForHour -= OneHour;
        }

        if (_deltaTimeForDay >= OneDay)
        {
            Debug.Log("OnOneGameDay");
            onOneDayElapse?.Invoke();
            _deltaTimeForDay -= OneDay;
        }

        if (_deltaTimeForDay >= OneYear)
        {
            Debug.Log("OnOneGameYear");
            onOneYearElapse?.Invoke();
            _deltaTimeForYear -= OneYear;
        }
        
        if(_deltaTimeForHour < 0)
            Debug.Log("bug");
    }

    private void OnApplicationQuit()
    {
        _lastTime = DateTime.Now;
        SaveGameTime();
    }
    
    /// <summary>
    /// @brief : 오프라인 보상
    ///          게임 종료후 흐르는 시간은 현실로 12시간이며 차원가속이 적용되지 않는다.
    ///          기본적으로 온라인의 30%만 받는다.
    /// @TODO :  특정 스트리머에 의해 %가 높아질 수 있음
    /// </summary>
    private void OfflineReward()
    {
        var offlineElapsedTime = _currentTime - _lastTime;
        var deltaRealTime = (float)offlineElapsedTime.TotalSeconds;
        float deltaGameTime = 0f;
        
        Debug.Log("마지막 접속 후 지난 실제 시간 : " + offlineElapsedTime);
        
        //
        if (deltaRealTime / 3600f > 3600f * 12f)
            deltaGameTime = 3600 * 12f;
        else
            deltaGameTime = deltaRealTime;                    
        //모니터링 보상
        BigInteger offlineMileage = GameObject.Find("Monitoring").GetComponent<CMonitoring>().CalculateMileage() *
                                    (int) ((deltaGameTime + _deltaTimeForHour) / _oneHour);
        _deltaTimeForHour %= _oneHour;
        var finalOfflineMileage= offlineMileage / 10 * 3;
        Mileage.Instance.Value += finalOfflineMileage; //30%
        Debug.Log("오프라인 보상 마일리지 : " + finalOfflineMileage);
        
        //편집 보상
        BigInteger offlineGold = GameObject.Find("VideoEdit").GetComponent<CVideoEdit>().CalculateGold() *
                                 (int) ((deltaGameTime + _deltaTimeForDay) / _oneDay);
        _deltaTimeForDay %= _oneDay;
        var finalOfflineGold= offlineGold / 10 * 3;
        Gold.Instance.Value += finalOfflineGold;
        Debug.Log("오프라인 보상 편집수당 : " + finalOfflineGold);
        
        //월급
        BigInteger salary = GameObject.Find("VideoEdit").GetComponent<CVideoEdit>().CalculateSalary() * (int)((deltaGameTime + _deltaTimeForYear) / _oneYear);
        _deltaTimeForYear %= _oneYear;
        Gold.Instance.Value += salary;
        Debug.Log(("오프라인 연봉" + salary));

    }

    private void SaveGameTime()
    {
        SavedTime times = new SavedTime
        {
            LastTime = _lastTime,
            DeltaTimeForDay = _deltaTimeForDay,
            DeltaTimeForHour = _deltaTimeForHour,
            DeltaTimeForYear = _deltaTimeForYear
        };

        CSaveLoadManager.Instance.CreateJsonFile(times,"SaveFiles","Times");
        Debug.Log("저장완료");
    }

    private void LoadGameTime()
    {
        var times = CSaveLoadManager.Instance.LoadJsonFile<SavedTime>("SaveFiles", "Times");;

        if (times == null)
        {
            _lastTime = DateTime.Now;
            _deltaTimeForDay = 0;
            _deltaTimeForHour = 0;
            _deltaTimeForYear = 0;
            return;
        }

        _lastTime = times.LastTime;
        _deltaTimeForDay = times.DeltaTimeForDay;
        _deltaTimeForHour = times.DeltaTimeForHour;
        _deltaTimeForYear = times.DeltaTimeForYear;
    }
}
