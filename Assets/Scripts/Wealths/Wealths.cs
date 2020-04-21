using System.Numerics;
using Util;

namespace Wealths
{
    /// <summary>
    /// @Singleton
    /// @brief : 플레이어가 보유한 캐릭터들을 성장시키는 재화
    /// @TODO : 현재까지는 다른 재화들과 코드가 다를바가 없음. 굳이 클래스를 따로 만들어야할까? 전역으로 사용되고 싱글톤이라 어쩔수 없긴한데 한번 생각해보자.
    /// </summary>
    public class Mileage : Singleton<Mileage>
    {
        public BigInteger Value { get; set; } = 0;

        //마일리지를 3자리수 + 소수점 한자리 + 알파벳으로 표현
        public string ConversedMileage()
        {
            return UnitConversion.ConverseUnit(Value).ConversedUnitToString();
        }
    }
    
    /// <summary>
    /// @Singleton
    /// @brief : 플레이어를 강화시키는 재화
   
    /// </summary>
    public class Money : Singleton<Money>
    {
        public BigInteger Value { get; set; } = 0;

        //돈을 3자리수 + 소수점 한자리 + 알파벳으로 표현
        public string ConversedMoney()
        {
            return UnitConversion.ConverseUnit(Value).ConversedUnitToString();
        }
    }
    
    /// <summary>
    /// @Singleton
    /// @brief : 유료 재화, 특정 아이템이나 편의성 버프를 구입시 사용
    /// </summary>
    public class Jewel : Singleton<Jewel>
    {
        public uint Value { get; set; } = 0;

        public string ConversedJewel()
        {
            return Value.ToString();
        }
    }
}