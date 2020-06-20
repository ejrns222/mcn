namespace Characters.Streamers
{
    public class CTestHun : StreamerBase
    {
        public override long Skill(long calculatedValue)
        {
            return (long) (Player.Instance.FindStreamer(EStreamer.TestHyun) ? calculatedValue * 0.3f : 0);
        }

        public CTestHun()
        {
            Tag = EStreamer.TestHun;
            Rank = ERank.F;
            IncreasingSubs = 2;
            Subscribers = 10;
            Expectation = 5000;
            AdLevel = 0;
            AdPrice = 25000;
        }
    }
}
