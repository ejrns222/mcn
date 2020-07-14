namespace Characters.Streamers
{
    public class CTestTaek : StreamerBase
    {
        public override long Skill(long calculatedValue)
        {
            return -100;
        }
        public CTestTaek()
        {
            Tag = EStreamer.TestTaek;
            Rank = ERank.E;
            IncreasingSubs = 3;
            Subscribers = 1;
            Expectation = 25000;
            AdLevel = 0;
            AdPrice = 100000;
        }
    }
}