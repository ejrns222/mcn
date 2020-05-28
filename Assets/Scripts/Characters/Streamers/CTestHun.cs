using System.Numerics;
using UnityEngine;
using Util;

namespace Characters.Streamers
{
    public class CTestHun : MonoBehaviour,IStreamer
    {
        public EStreamer Tag { get; } = EStreamer.TestHun;

        public BigInteger Skill(BigInteger calculatedValue)
        {
            return Player.Instance.FindStreamer(EStreamer.TestHyun) ? BigInteger.Divide(calculatedValue,10)*3 : 0;
        }

        public ERank Rank { get; } = ERank.F;
        public uint IncreaseSubs { get;  } = 2;
        public uint Subscribers { get; set; } = 10;
        public uint Expectation { get; set; } = 5000;
        public uint AdLevel { get; set; } = 1;
        public BigInteger AdPrice { get; } = 25000;
    }
}
