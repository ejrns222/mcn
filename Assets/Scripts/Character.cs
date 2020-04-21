using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EStreamer
{
    TestHun,
    TestHyun,
}

[Serializable]
public class Streamer 
{
    public EStreamer Tag
    {
        get;
        set;
    }

    
    public static Streamer MakeStreamer(EStreamer eStreamer)
    {
        switch (eStreamer)
        {
            case EStreamer.TestHun:
                return new TestHun(EStreamer.TestHun);
            case EStreamer.TestHyun:
                return new TestHyun(EStreamer.TestHyun);
        }

        return null;
    }

    public virtual uint Skill()
    {
        return 0;
    }

    public virtual void TestLog()
    {
        Debug.Log("class : streamer");
    }
}



public class TestHun : Streamer
{
    

    public TestHun(EStreamer tag)
    {
        Tag = tag;
    }

    public override void TestLog()
    {
        Debug.Log("class : TestHun");
    }

    public override uint Skill()
    {
        if (Player.Instance.FindStreamer(EStreamer.TestHyun))
        {
            return 1;
        }

        return 0;
    }
}

public class TestHyun : Streamer
{
    public TestHyun(EStreamer tag)
    {
        Tag = tag;
    }
    public override void TestLog()
    {
        Debug.Log("class : TestHyun");
    }

    public override uint Skill()
    {
        return 1;
    }
}