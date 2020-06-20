namespace Characters.Streamers
{
    public class CTestHyun : StreamerBase
    {
        public override long Skill(long calculatedValue)
        {
            return calculatedValue/10;
        }
        public CTestHyun()
        {
            Tag = EStreamer.TestHyun;
            Rank = ERank.E;
            IncreasingSubs = 5;
            Subscribers = 1000;
            Expectation = 50000;
            AdLevel = 0;
            AdPrice = 55000;
        }
    }
}