using System.Numerics;

namespace Characters.Streamers
{
    public class CTestTaek : IStreamer
    {
        public EStreamer Tag { get; } = EStreamer.TestTaek;

        public BigInteger Skill(BigInteger calculatedValue)
        {
            return -200;
        }

        public ERank Rank { get; } = ERank.E;
        public uint IncreaseSubs { get; } = 10;
        public uint Subscribers { get; set; } = 20;
        public uint Expectation { get; set; } = 8000;
        public uint AdLevel { get; set; } = 1;
        public BigInteger AdPrice { get; } = 180000;
    }
}