using System;
using System.Numerics;

namespace Util
{
    public enum EStreamer
    {
        TestHun,
        TestHyun,
        TestTaek,
    }

    public enum ERank
    {
        F,E,D,C,B,A,
    }
    


    public interface IStreamer
    {
        EStreamer Tag
        {
            get;
        }
    
        BigInteger Skill(BigInteger calculatedValue);

        //등급
        ERank Rank
        {
            get;
        }

        //초기 구독자 수
        uint IncreaseSubs
        {
            get;
        }
        
        //현재 구독자 수
        uint Subscribers
        {
            get;
            set;
        }

        //성장기대치
        uint Expectation
        {
            get;
            set;
        }

        uint AdLevel
        {
            get;
            set;
        }
        //광고비용
        BigInteger AdPrice
        {
            get;
        }
        
    }
    
}