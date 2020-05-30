using System;
using System.Numerics;

namespace Characters
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

        //구독자 증가 폭
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

        //광고 레벨 : 구독자 수 증가 폭과 관련
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