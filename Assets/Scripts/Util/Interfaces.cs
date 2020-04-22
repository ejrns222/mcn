namespace Util
{
    public enum EStreamer
    {
        TestHun,
        TestHyun,
    }

    public interface IStreamer 
    {
        EStreamer Tag
        {
            get;
        }
    
        uint Skill();
    
        /*public static Streamer MakeStreamer(EStreamer eStreamer)
    {
        switch (eStreamer)
        {
            case EStreamer.TestHun:
                return new TestHun(EStreamer.TestHun);
            case EStreamer.TestHyun:
                return new TestHyun(EStreamer.TestHyun);
        }

        return null;
    }*/
    }
}