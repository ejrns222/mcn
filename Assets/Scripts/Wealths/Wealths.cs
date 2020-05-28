using System;
using System.Numerics;
using Util;

namespace Wealths
{
    interface IWealth
    {
        String ConversedUnit();
        
    }
    /// <summary>
    /// @Singleton
    /// @brief : 플레이어가 보유한 캐릭터들을 성장시키는 재화
    /// @TODO : 클래스 합치기
    /// </summary>
    public class Mileage : Singleton<Mileage>,IWealth
    {
        public BigInteger Value { get; set; } = 0;

        //마일리지를 3자리수 + 소수점 한자리 + 알파벳으로 표현
        public string ConversedUnit()
        {
            return UnitConversion.ConverseUnit(Value).ConversedUnitToString();
        }
    }
    
    /// <summary>
    /// @Singleton
    /// @brief : 플레이어를 강화시키는 재화
    /// </summary>
    public class Gold : Singleton<Gold>,IWealth
    {
        public BigInteger Value { get; set; } = 0;

        //돈을 3자리수 + 소수점 한자리 + 알파벳으로 표현
        public string ConversedUnit()
        {
            return UnitConversion.ConverseUnit(Value).ConversedUnitToString();
        }
    }
    
    /// <summary>
    /// @Singleton
    /// @brief : 유료 재화, 특정 아이템이나 편의성 버프를 구입시 사용
    /// </summary>
    public class Jewel : Singleton<Jewel>,IWealth
    {
        public uint Value { get; set; } = 0;

        public string ConversedUnit()
        {
            return Value.ToString();
        }
    }
}