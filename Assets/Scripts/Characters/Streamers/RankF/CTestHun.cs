namespace Characters.Streamers
{
    public class CTestHun : StreamerBase
    {
        public CTestHun()
        {
            Tag = EStreamer.TestHun;
            Name = "BJ훈";
            Rank = ERank.F;
            IncreasingSubs = 2;
            Subscribers = 100;
            Expectation = 5000;
            AdLevel = 0;
            AdPrice = 25000;
        }
    }
}
