using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Characters;
using UnityEngine;
using UnityEngine.UI;
using Util;
using Wealths;

public class CEditButton : MonoBehaviour
{
    public Button button;
    public Text buttonText;
    public IStreamer streamer;
    public BigInteger price;
    public Image image;
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
        if (Gold.Instance.Value >= streamer.AdPrice *
            (BigInteger) Math.Pow(1.12f, (int) streamer.AdLevel))
        {
            Gold.Instance.Value -= streamer.AdPrice *
                                   (BigInteger) Math.Pow(1.12f, (int) streamer.AdLevel);
            streamer.AdLevel++;
            
            return;
        }
        
        var pw = Instantiate(Resources.Load("UIPrefabs/PopUpWindow") as GameObject,transform.root);
                
        if (pw != null)
        {
            /*var text = pw.transform.GetChild(1).Find("Content").gameObject.GetComponent<Text>();
            pw.transform.SetParent(GameObject.Find("VideoEdit").transform);
            text.text = "골드가 부족하군..";*/
            pw.GetComponent<CPopUpWindow>().SetText("골드가 부족하군..");
        }
        
    }
}
