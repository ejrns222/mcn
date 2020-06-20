namespace Characters.Streamers
{
    public class CGun : StreamerBase
    {
        public override long Skill(long calculatedValue)
        {
            return 0;
        }
        public CGun()
        {
            Tag = EStreamer.Gun;
            Rank = ERank.D;
            IncreasingSubs = 4;
            Subscribers = 500;
            Expectation = 12000;
            AdLevel = 0;
            AdPrice = 25000;
        }
    }
}
