namespace Characters.Streamers
{
    public class CSajang : StreamerBase
    {
        public CSajang()
        {
            Tag = EStreamer.Sajang;
            Name = "BJ대표";
            Rank = ERank.A;
            IncreasingSubs = 10;
            Subscribers = 20000;
            Expectation = 1000000;
            AdLevel = 0;
            AdPrice = 1000000;
        }
    }
}