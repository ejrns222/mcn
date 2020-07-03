using System;
using UnityEngine;
using UnityEngine.UI;

namespace Util
{
    /// <summary>
    /// @brief : JsonUtility로 DateTime을 Serialize할 수 없음. 이걸 쓰면 가능 
    /// </summary>
    class JsonDateTime {
        public long Value; //제이슨은 프라이빗은 저장하지 않는 듯
        public static implicit operator DateTime(JsonDateTime jdt) {
            return DateTime.FromFileTime(jdt.Value);
        }
        public static implicit operator JsonDateTime(DateTime dt) {
            var jdt = new JsonDateTime {Value = dt.ToFileTime()};
            return jdt;
        }
    }

    

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
            public ulong TotalPlayingTime;
            public string LastTime;
        }
    
        public static CTimeManager Instance = null;
        private DateTime _currentTime;
        public static DateTime LastRealTime; //마지막에 게임을 종료했을 때의 실제시간을 저장

        private float TimeFactor => CDimensionResearch.ResearchSkill.DAccelLevel / 40f; //클수록 시간이 빠르게 흐른다.
    
        private const float _oneHour = 1f;
        private const float _oneDay = 24f;
        private const float _oneYear = 8640f;


        private float _deltaTimeForHour;//저장
        private float _deltaTimeForDay;//저장
        private float _deltaTimeForYear;//저장
        private ulong _totalPlayingGameHour;//저장, 첫게임 시작부터 계속 흐른다. TODO : 처음에 플레이어의 이름을 받아서 이름이 철수면 철수력 몇년몇월몇일 이렇게 하면 될듯

        public float DeltaTimeForHour => _deltaTimeForHour;
        public float DeltaTimeForDay => _deltaTimeForDay;
        public float DeltaTimeForYear => _deltaTimeForYear;

        public float OneHour => _oneHour* (1f - TimeFactor);
        public float OneDay => _oneDay * (1f - TimeFactor);
        public float OneYear => _oneYear * (1f - TimeFactor);
    
        public delegate void OneTimeHandler();

        public OneTimeHandler onOneDayElapse;
        public OneTimeHandler onOneHourElapse;
        public OneTimeHandler onOneYearElapse;

        public Text clockText;

        public long todayOfflineSalary;
        public long todayOfflineMileage;
        public long todayOfflineGold;

        void Awake()
        {
            if (!Instance)
                Instance = this;
            else if(Instance != this)
                Destroy(gameObject);

            _currentTime = DateTime.Now;
            _deltaTimeForHour = 0;
            _deltaTimeForDay = 0;
            _deltaTimeForYear = 0;
            _totalPlayingGameHour = 0;
            LoadGameTime();
        
            Debug.Log(_currentTime);
            OfflineReward();
        }

        private void Update()
        {
            //TODO : 델타타임은 Hour만 남기고 나머진 TotalPlayingHour로 계산하자. 뭔가 이상한듯 연봉을 날짜 중간에 막 받고 그럼
            _deltaTimeForHour += Time.deltaTime;
            _deltaTimeForDay += Time.deltaTime;
            _deltaTimeForYear += Time.deltaTime;

            var clock = CalcGameTime(_totalPlayingGameHour);
            clockText.text = $"{clock[0]}년 {clock[1]}월 {clock[2]}일 {clock[3]}시";
        
            if (_deltaTimeForHour >= OneHour)
            { 
                Debug.Log("OnOneGameHour");
                onOneHourElapse?.Invoke();
                _deltaTimeForHour -= OneHour;
                _totalPlayingGameHour++;
            }

            if (_deltaTimeForDay >= OneDay)
            {
                Debug.Log("OnOneGameDay");
                onOneDayElapse?.Invoke();
                GameObject.Find("Monitoring")?.GetComponent<CMonitoring>()?.Refresh();
                _deltaTimeForDay -= OneDay;
            }

            if (_deltaTimeForDay >= OneYear)
            {
                Debug.Log("OnOneGameYear");
                onOneYearElapse?.Invoke();
                _deltaTimeForYear -= OneYear;
            }
        
            if(_deltaTimeForHour < 0)
                Debug.Log("Time Bug");
        }

        private void OnApplicationQuit()
        {
            LastRealTime = DateTime.Now;
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
            if ((DateTime.Now - LastRealTime).TotalSeconds < 3)
            {
                return;
            }
            var offlineElapsedTime = _currentTime - LastRealTime;
            var deltaRealSecond = (float)offlineElapsedTime.TotalSeconds;
            float deltaGameHour = 0f;
        
            Debug.Log("마지막 접속 후 지난 실제 시간 : " + offlineElapsedTime.TotalHours);
        
            //실제 1초 = 게임내 1시간 , 실제 12시간보다 크다면 최대보상인 12시간으로 계산
            if (deltaRealSecond / 3600f > 12f)
                deltaGameHour = 3600 * 12f;
            else
                deltaGameHour = deltaRealSecond;

            var lastTotalGameHour = _totalPlayingGameHour;
            _totalPlayingGameHour += (ulong)deltaGameHour;
            
            //모니터링 보상 TODO : 나중에 계산 다시
            long offlineMileage = GameObject.Find("Monitoring").GetComponent<CMonitoring>().CalculateMileage(false) *
                                  (int) ((deltaGameHour + _deltaTimeForHour) / _oneHour);
            _deltaTimeForHour = (deltaGameHour + _deltaTimeForHour) % _oneHour;
            todayOfflineMileage= offlineMileage / 10 * 3;
            Player.Instance.mileage += todayOfflineMileage; //30%
            Debug.Log("오프라인 보상 마일리지 : " + todayOfflineMileage);
        
            //편집 보상
            long offlineGold = GameObject.Find("VideoEdit").GetComponent<CVideoEdit>().CalculateGold() *
                               (int) ((deltaGameHour + _deltaTimeForDay) / _oneDay);
            _deltaTimeForDay = (deltaGameHour + _deltaTimeForDay) % _oneDay;
            todayOfflineGold= offlineGold / 10 * 3;
            Player.Instance.gold += todayOfflineGold;
            Debug.Log("오프라인 보상 편집수당 : " + todayOfflineGold);
        
            //월급
            todayOfflineSalary = GameObject.Find("VideoEdit").GetComponent<CVideoEdit>().CalculateSalary() * (int)((deltaGameHour + _deltaTimeForYear) / _oneYear);
            _deltaTimeForYear = (deltaGameHour + _deltaTimeForYear) % _oneYear;
            Player.Instance.gold += todayOfflineSalary;
            Debug.Log(("오프라인 연봉" + todayOfflineSalary));
            
        }

        private void SaveGameTime()
        {
            SavedTime times = new SavedTime
            {
                LastTime = JsonUtility.ToJson((JsonDateTime)LastRealTime),
                DeltaTimeForDay = _deltaTimeForDay,
                DeltaTimeForHour = _deltaTimeForHour,
                DeltaTimeForYear = _deltaTimeForYear,
                TotalPlayingTime = _totalPlayingGameHour
            };

            CSaveLoadManager.CreateJsonFile(times,"SaveFiles","Times");
            Debug.Log("저장완료");
        }

        private void LoadGameTime()
        {
            var times = CSaveLoadManager.LoadJsonFile<SavedTime>("SaveFiles", "Times");;

            if (times == null)
            {
                LastRealTime = DateTime.Now;
                return;
            }

            LastRealTime = JsonUtility.FromJson<JsonDateTime>(times.LastTime);
            Debug.Log("마지막 접속 날짜 : " + LastRealTime);
            _deltaTimeForDay = times.DeltaTimeForDay;
            _deltaTimeForHour = times.DeltaTimeForHour;
            _deltaTimeForYear = times.DeltaTimeForYear;
            _totalPlayingGameHour = times.TotalPlayingTime;
            
        }

        private uint[] CalcGameTime(ulong totalHour)
        {
            uint[] ret = new uint[4];
            ret[0] = (uint)totalHour / (uint)_oneYear;
            ret[1] = ((uint)totalHour - (ret[0] * (uint)_oneYear)) / ((uint)_oneDay * 30) +1;
            ret[2] = ((uint)totalHour - (ret[0] * (uint)_oneYear) - ((ret[1]-1) * (uint)_oneDay*30)) / (uint)_oneDay;
            ret[3] = ((uint)totalHour - (ret[0] * (uint)_oneYear) - ((ret[1]-1) * (uint)_oneDay * 30) - (ret[2] * (uint)_oneDay)) / (uint)_oneHour;
            return ret;
        }
    }
}