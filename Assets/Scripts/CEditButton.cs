using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Util;
using Wealths;

public class CEditButton : MonoBehaviour
{
    public Button button;
    public Text buttonText;
    public GameObject streamer;
    public BigInteger price;
    // Start is called before the first frame update
    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    private void OnClick()
    {
        var tmp = streamer.GetComponent<IStreamer>();
        if (Gold.Instance.Value >= tmp.AdPrice *
            (BigInteger) Math.Pow(1.12f, (int) tmp.AdLevel))
        {
            Gold.Instance.Value -= tmp.AdPrice *
                                   (BigInteger) Math.Pow(1.12f, (int) tmp.AdLevel);
            tmp.AdLevel++;
            
            return;
        }
        
        var pw = Instantiate(Resources.Load("PopUpWindow") as GameObject,transform);
                
        if (pw != null)
        {
            var text = pw.transform.GetChild(1).Find("Content").gameObject.GetComponent<Text>();
            pw.transform.SetParent(GameObject.Find("VideoEdit").transform);
            text.text = "골드가 부족하군..";
        }
        
    }
}
