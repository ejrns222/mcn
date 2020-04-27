using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wealths;

//TODO:WealthRenderer랑 합치고 싶음 
public class CEarnedWealthRenderer : MonoBehaviour
{
    private Text _text;
    public uint value = 0;
    
    
    void Start()
    {
        _text = GetComponent<Text>();
        _text.text = UnitConversion.ConverseUnit(value).ConversedUnitToString();
        transform.localScale = new Vector3(1,1,1);
        //transform.localPosition = new Vector3(0,0,0);
        
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(0,0.2f,0);
        var color = _text.color;
        color = new Color(color.r,color.g,color.b,color.a - Time.deltaTime);
        _text.color = color;
        
        if(color.a < 0)
            Destroy(gameObject);
    }
}
