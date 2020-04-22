using UnityEngine;
using Util;

namespace Characters.Streamers
{
    public class CTestHun : MonoBehaviour,IStreamer
    {
        public EStreamer Tag { get; } = EStreamer.TestHun;

        public uint Skill()
        {
            return Player.Instance.FindStreamer(EStreamer.TestHyun) ? (uint) 1 : (uint) 0;
        }
    }
}
