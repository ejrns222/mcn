using System.Numerics;
using UnityEngine;
using Util;

namespace Wealths
{
    /// <summary>
    /// @Singleton
    /// @brief : 플레이어가 보유한 캐릭터들을 성장시키는 재화
    /// </summary>
    public class CMileage : Singleton<CMileage>
    {
        public BigInteger Value { get; set; } = 0;

        //마일리지를 3자리수 + 소수점 한자리 + 알파벳으로 표현
        public string ConversedMileage()
        {
            return UnitConversion.ConverseUnit(Value).ConversedUnitToString();
        }
    
    }
}