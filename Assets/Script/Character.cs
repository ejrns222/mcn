using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EStreamer
{
    TestHun,
    TestHyun,
}

public class Streamer
{
    public static Streamer MakeStreamer(EStreamer eStreamer)
    {
        switch (eStreamer)
        {
            case EStreamer.TestHun:
                return new TestHun();
            case EStreamer.TestHyun:
                return new TestHyun();
        }

        return null;
    }

    public virtual void TestLog()
    {
        Debug.Log("class : streamer");
    }
}

public class Neotuber
{
    
}

public class TestHun : Streamer
{
    public override void TestLog()
    {
        Debug.Log("class : TestHun");
    }
}

public class TestHyun : Streamer
{
    public override void TestLog()
    {
        Debug.Log("class : TestHyun");
    }
}