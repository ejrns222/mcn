namespace Characters.Streamers
{
    public class CTaegong : StreamerBase
    {
        public CTaegong()
        {
            Tag = EStreamer.Taegong;
            Name = "BJ태공";
            Rank = ERank.B;
            IncreasingSubs = 7;
            Subscribers = 10000;
            Expectation = 500000;
            AdLevel = 0;
            AdPrice = 700000;
        }
    }
}