namespace Characters.Streamers
{
    public class CTestHyun : StreamerBase
    {
        public CTestHyun()
        {
            Tag = EStreamer.TestHyun;
            Name = "BJ현";
            Rank = ERank.C;
            IncreasingSubs = 5;
            Subscribers = 5000;
            Expectation = 100000;
            AdLevel = 0;
            AdPrice = 500000;
        }
    }
}