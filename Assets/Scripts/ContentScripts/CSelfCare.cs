using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Wealths;

public class CSelfCare : MonoBehaviour
{
    private Button[] _buttons;

    public uint EditLevel 
    { 
        get;
        private set;
    } = 0;

    public uint ComputerLevel
    {
        get;
        private set;
    } = 0;

    public uint BotLevel
    {
        get;
        private set;
    } = 0;

    public uint SocialLevel
    {
        get;
        private set;
    } = 0;

    public uint BroadCastLevel
    {
        get;
        private set;
    } = 0;
    
    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>();

        {
            _buttons[0].onClick.AddListener(VideoEditStudy);
            _buttons[1].onClick.AddListener(ComUpgrade);
            _buttons[2].onClick.AddListener(BotUpgrade);
            _buttons[3].onClick.AddListener(SocialLevelUp);
            _buttons[4].onClick.AddListener(BroadCastUp);

            _buttons[0].transform.GetChild(0).GetComponent<Text>().text = "영상 편집 공부";
            _buttons[1].transform.GetChild(0).GetComponent<Text>().text = "컴퓨터 업그레이드";
            _buttons[2].transform.GetChild(0).GetComponent<Text>().text = "챗봇 명령어 추가";
            _buttons[3].transform.GetChild(0).GetComponent<Text>().text = "사교계 진출";
            _buttons[4].transform.GetChild(0).GetComponent<Text>().text = "방송 상위노출";
        }
        
        gameObject.SetActive(false);
    }

    //TODO : 나중에 UI생성하는 것들은 한곳에 static함수로 모으면 좋을 것 같다.
    private void NotEnoughGold()
    {
        var pw = Instantiate(Resources.Load("UIPrefabs/PopUpWindow") as GameObject,transform.root);
                
        if (pw != null)
        {
            pw.GetComponent<CPopUpWindow>().SetText("골드가 부족하군..");
        }
    }

    /// <summary>
    /// @brief : 영상 편집 수당 증가
    /// </summary>
    private void VideoEditStudy()
    {
        BigInteger price = 10000 + (5000 * EditLevel);
        if (Gold.Instance.Value < price)
        {
            NotEnoughGold();
            return;
        }

        Gold.Instance.Value -= price;
        EditLevel++;
    }

    private void ComUpgrade()
    {
        BigInteger price = 10000 + (5000 * EditLevel);
        if (Gold.Instance.Value < price)
        {
            NotEnoughGold();
            return;
        }

        Gold.Instance.Value -= price;
        ComputerLevel++;
    }
    
    private void BotUpgrade()
    {
        BigInteger price = 10000 + (5000 * EditLevel);
        if (Gold.Instance.Value < price)
        {
            NotEnoughGold();
            return;
        }

        Gold.Instance.Value -= price;
        BotLevel++;
    }

    private void SocialLevelUp()
    {
        BigInteger price = 10000 + (5000 * EditLevel);
        if (Gold.Instance.Value < price)
        {
            NotEnoughGold();
            return;
        }

        Gold.Instance.Value -= price;
        SocialLevel++;
    }

    private void BroadCastUp()
    {
        BigInteger price = 10000 + (5000 * EditLevel);
        if (Gold.Instance.Value < price)
        {
            NotEnoughGold();
            return;
        }

        Gold.Instance.Value -= price;
        BroadCastLevel++;
    }
    
    
}
