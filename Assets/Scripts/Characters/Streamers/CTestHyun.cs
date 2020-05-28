using System.Numerics;
using UnityEngine;
using Util;

namespace Characters.Streamers
{
    public class CTestHyun : MonoBehaviour,IStreamer
    {
        public EStreamer Tag { get; } = EStreamer.TestHyun;

        public BigInteger Skill(BigInteger calculatedValue)
        {
            return BigInteger.Divide(calculatedValue,10);
        }

        public ERank Rank { get; } = ERank.E;
        public uint IncreaseSubs { get; } = 12;
        public uint Subscribers { get; set; } = 50;
        public uint Expectation { get; set; } = 10000;
        public uint AdLevel { get; set; } = 1;
        public BigInteger AdPrice { get; } = 200000;
    }
}