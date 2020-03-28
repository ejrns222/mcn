using System;
using System.Collections;
using System.Collections.Generic;

public abstract class Charactor
{
    protected uint Subscriber { get; set; } //나중에 set을 특정 효과에따라 증감하게 바꿔야 함
    protected string Name;
    protected string Desc;
    
    public abstract bool Init(string name, uint numSubs);
   
}

public class Streamer : Charactor
{
    public override bool Init(string name, uint numSubs)
    {
        Name = name;
        Subscriber = numSubs;
        return true;
    }
}

public class Neotuber : Charactor
{
    public override bool Init(string name, uint numSubs)
    {
        Name = name;
        Subscriber = numSubs;
        return true;
    }
}

public class DDeokuni : Streamer
{
    public void Update(ref UInt64 mileage)
    {
        mileage *= 2;
    }
}