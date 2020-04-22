using UnityEngine;
using Util;

namespace Characters.Streamers
{
    public class CTestHyun : MonoBehaviour,IStreamer
    {
        public EStreamer Tag { get; } = EStreamer.TestHyun;

        public uint Skill()
        {
            return 1;
        }
    }
}