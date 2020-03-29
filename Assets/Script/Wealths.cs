using System;
using System.Collections;
using System.Collections.Generic;

public abstract class Wealths
{
    public uint Count;
    protected uint Increase;
    public delegate void CalcFuncDelegate(ref uint factor);//델리게이트에 계수를 조정하는 함수를 담아서 쓸 수 있게 만들어야함 
    
    public abstract void Calculate();
}

public class Mileage : Wealths
{
   
    
    public override void Calculate()
    {
        
    }
}