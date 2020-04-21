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
    
    static class UnitConversion
    {
        public static ConversedUnit ConverseUnit(BigInteger bigInteger)
        {
            string[] charNums = {"", "A", "B", "C", "D", "E"};
            ConversedUnit ret;
            ret.CharNum = charNums[0];
            ret.RealNum = bigInteger.ToString();
            
            if (ret.RealNum.Length < 4) return ret;
            
            var devidedNum = (uint)((ret.RealNum.Length-1)/3);
            ret.CharNum = charNums[devidedNum];
            switch (ret.RealNum.Length % 3)
            {
                case 0:
                    ret.RealNum = ret.RealNum.Substring(0, 3) + "." + ret.RealNum.Substring(3,1);
                    break;
                case 1:
                    ret.RealNum = ret.RealNum.Substring(0, 1) + "." + ret.RealNum.Substring(1,1);
                    break;
                case 2:
                    ret.RealNum = ret.RealNum.Substring(0, 2) + "." + ret.RealNum.Substring(2,1);
                    break;
            }
            

            return ret;
        }
    }
}
