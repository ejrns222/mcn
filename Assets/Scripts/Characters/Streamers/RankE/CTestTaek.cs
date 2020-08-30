namespace Characters.Streamers
{
    public class CTestTaek : StreamerBase
    {
        public CTestTaek()
        {
            Tag = EStreamer.TestTaek;
            Name = "BJ택";
            Rank = ERank.E;
            IncreasingSubs = 3;
            Subscribers = 500;
            Expectation = 25000;
            AdLevel = 0;
            AdPrice = 100000;
        }
    }
}