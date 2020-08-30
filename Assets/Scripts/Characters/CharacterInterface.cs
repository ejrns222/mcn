//TODO : 방송 시작, 끝시간도 정하자 그러면 배치를 사람들이 신경써서 할 듯 저녁~밤시간대가 제일 많고 아침시간대 스트리머는 거의 없는 걸로 하면 좋을것같다. 

using System;
using System.Reflection;

namespace Characters
{
    public enum EStreamer
    {
        TestHun,
        TestHyun,
        TestTaek,
        Gun,
        Sajang,
        Taegong,
    }

    public enum EInstructor
    {
        Daedo,
        SsuckSso,
        Gangzi,
        KimBlue,
        Ryueumi,
    }

    public enum ERank
    {
        A = 0,B,C,D,E,F,
    }


    public class StreamerBase 
    {
        public string Info()
        {
            return $"이름 : {Name}\n랭크 : {Rank.ToString()}\n광고 횟수 : {AdLevel}\n구독자 수 : {Subscribers}\n졸업요건 : {Expectation}";
        }
        public EStreamer Tag { get; protected set; }//태그
        public string Name;
        public ERank Rank { get; protected set; }//랭크
        public uint IncreasingSubs { get; protected set; }//구독자 증가 폭
        public uint Subscribers { get; set; }//현재 구독자 수
        public uint Expectation { get; protected set; }//성장 기대치, 이 이상으로 구독자가 증가하지 않는다.
        public uint AdLevel { get; set; }//광고 레벨, 구독자 증가 폭에 영향을 준다
        public long AdPrice { get; protected set; }//0레벨 기준 광고 비용, 광고 레벨이 높아질수록 비싸진다
        public int StartTime { get; protected set; }
        public int EndTime { get; protected set; }
    }

    public static class StreamerBaseForJson
    {
        public static string StreamerToString(StreamerBase streamer)
        {
            if (streamer == null)
                return "null";
            return streamer.Tag + "," + streamer.Subscribers + "," + streamer.AdLevel;
        }

        public static StreamerBase StringToStreamer(string loadedJson)
        {
            if (loadedJson == "null")
                return null;
            string[] ret = loadedJson.Split(',');
            Type t = Assembly.GetExecutingAssembly().GetType("Characters.Streamers.C" + ret[0]);
            StreamerBase streamer = (StreamerBase) Activator.CreateInstance(t);
            streamer.Subscribers = uint.Parse(ret[1]);
            streamer.AdLevel = uint.Parse(ret[2]);
            return streamer;
        }
    }

    public abstract class InstructorBase
    {
        public abstract long Skill(long calculatedValue);
        public string SkillName { get; protected set; }
        public string SkillDesc { get; protected set; }
        public EInstructor Tag { get; protected set; }
        public string Desc { get; protected set; }
        public string Name { get; protected set; }
    }

    public class CharacterUtil
    {
        public static string FindName(string nameTag)
        {
            EStreamer tag = (EStreamer) Enum.Parse(typeof(EStreamer), nameTag);
            foreach (var v in CDictionary.AllStreamers.Keys)
            {
                if (v.Tag == tag)
                {
                    return v.Name;
                }
            }

            return "";
        }
    }
}