using System;
using System.Collections;
using System.Collections.Generic;

public abstract class Wealths
{
    public uint Count;
    public float Factor;
    public abstract void Calculate();
}

public class Mileage : Wealths
{
    public override void Calculate()
    {
        
    }
}