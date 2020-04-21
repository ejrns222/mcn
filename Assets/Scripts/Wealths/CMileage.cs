using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Wealths
{
    /// <summary>
    /// @Singleton
    /// @brief : 플레이어가 보유한 캐릭터들을 성장시키는 재화
    /// @TODO : 다른 재화와 공유되는 필드와 메소드를 인터페이스화
    /// </summary>
    public class CMileage : MonoBehaviour
    {
        private static CMileage _instance;
       
        private BigInteger _value = 0;

        public BigInteger Value
        {
            get => _value;
            set => _value = value;
        }

        public static CMileage Instance
        {
            get
            {
                if (_instance != null) return _instance;
            
                var obj = FindObjectOfType<CMileage>();
                if (obj != null)
                {
                    _instance = obj;
                }
                else
                {
                    var newSingleton = new GameObject("Mileage").AddComponent<CMileage>();
                    _instance = newSingleton;
                }

                return _instance;
            }
            private set => _instance = value;
        }

        //마일리지를 3자리수 + 소수점 한자리 + 알파벳으로 표현
        public string ConversedMileage()
        {
            return UnitConversion.ConverseUnit(Value).ConversedUnitToString();
        }
    
    }
}