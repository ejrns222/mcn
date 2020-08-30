namespace Characters.Streamers
{
    public class CGun : StreamerBase
    {
        public CGun()
        {
            Tag = EStreamer.Gun;
            Name = "BJ건";
            Rank = ERank.D;
            IncreasingSubs = 4;
            Subscribers = 2500;
            Expectation = 50000;
            AdLevel = 0;
            AdPrice = 200000;
        }
    }
}
