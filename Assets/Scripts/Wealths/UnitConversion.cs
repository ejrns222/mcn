using System.Numerics;

namespace Wealths
{
    public struct ConversedUnit
    {
        public string RealNum;
        public string CharNum;

        public string ConversedUnitToString()
        {
            return RealNum + CharNum;
        }
    }
    /// <summary>
    /// @brief : bigInteger를 3자리숫자 + 소수점 한자리 + 알파벳으로 표현 (탭타이탄식 단위)
    /// </summary>
    static class UnitConversion
    {
        public static ConversedUnit ConverseUnit(long Integer)
        {
            string[] charNums = {"", "A", "B", "C", "D", "E", "F", "G", "H"};
            ConversedUnit ret;
            ret.CharNum = charNums[0];
            ret.RealNum = Integer.ToString();

            if (Integer >= 0)
            {
                if (ret.RealNum.Length < 4) return ret;

                var devidedNum = (uint) ((ret.RealNum.Length - 1) / 3);
                ret.CharNum = charNums[devidedNum];
                switch (ret.RealNum.Length % 3)
                {
                    case 0:
                        ret.RealNum = ret.RealNum.Substring(0, 3) + "." + ret.RealNum.Substring(3, 1);
                        break;
                    case 1:
                        ret.RealNum = ret.RealNum.Substring(0, 1) + "." + ret.RealNum.Substring(1, 1);
                        break;
                    case 2:
                        ret.RealNum = ret.RealNum.Substring(0, 2) + "." + ret.RealNum.Substring(2, 1);
                        break;
                }
            }
            else
            {
                if (ret.RealNum.Length < 5)
                {
                    ret.RealNum = ret.RealNum.Substring(1);
                    return ret;
                }
                
                var devidedNum = (uint) ((ret.RealNum.Length - 2) / 3);
                ret.CharNum = charNums[devidedNum];
                switch ((ret.RealNum.Length-1) % 3)
                {
                    case 0:
                        ret.RealNum = ret.RealNum.Substring(1, 3) + "." + ret.RealNum.Substring(4, 1);
                        break;
                    case 1:
                        ret.RealNum = ret.RealNum.Substring(1, 1) + "." + ret.RealNum.Substring(2, 1);
                        break;
                    case 2:
                        ret.RealNum = ret.RealNum.Substring(1, 2) + "." + ret.RealNum.Substring(3, 1);
                        break;
                }

                ret.RealNum = "-" + ret.RealNum;
            }
            return ret;
        }
    }
}
