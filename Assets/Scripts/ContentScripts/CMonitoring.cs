using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;
using Wealths;
using System.Numerics;
using Characters;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class MonitoringButton
{
    public Button Button;
    public Image Image;
    public Text SlotText;
    public Text PriceText;
    public IStreamer Streamer;
    public bool IsLocked;
    public bool IsOpen;
    public BigInteger Price;
    public int Idx;
}

public class CMonitoring : MonoBehaviour
{
    //private List<MonitoringButton> _monitoringButtons;
    private MonitoringButton[] _monitoringButtons;
    public GameObject characterChangerPopupWindow;

    private void Awake()
    {
        //_monitoringButtons = new List<MonitoringButton>(8);
        _monitoringButtons = new MonitoringButton[8];
        
        var slots = transform.GetChild(0).GetChild(0).GetChild(0);
        for (int i = 0; i < slots.childCount; i++)
        {
            var v = slots.GetChild(i);
            var tmp = new MonitoringButton
            {
                Button = v.GetComponentInChildren<Button>(),
                SlotText = v.transform.Find("Text").GetComponent<Text>(),
                PriceText = v.transform.Find("Button").Find("priceText").GetComponent<Text>(),
                Idx = i
            };
            tmp.SlotText.text = "Lock";
            tmp.IsLocked = true;
            tmp.IsOpen = false;
            tmp.Image = v.transform.GetChild(1).GetComponent<Image>();
            
            //_monitoringButtons.Add(tmp);
            _monitoringButtons[i] = tmp;
        }
        
        /*foreach (var v in transform.GetChild(0).GetComponentsInChildren<Button>())
        {
            var tmp = new MonitoringButton
            {
                Button = v,
                ButtonText = v.transform.Find("Text").GetComponent<Text>(),
                PriceText = v.transform.Find("priceText").GetComponent<Text>()
            };
            tmp.ButtonText.text = "Lock";
            tmp.IsLocked = true;
            tmp.IsOpen = false;
            tmp.Image = v.transform.GetChild(1).GetComponent<Image>();
            
            
            _monitoringButtons.Add(tmp);
        }*/

        for (var i = 0; i < Player.Instance.maxNumStreamers; i++)
        {
            var monitoringButton = _monitoringButtons[i];
            monitoringButton.SlotText.text = "Empty";
            monitoringButton.IsLocked = false; //잠김유무 : 스트리머를 등록 할 수 있느냐 없느냐
            monitoringButton.IsOpen = true; //구매가능 여부 : 언락한 다음버튼까지만 눌렀을 때 이벤트 발생함 
        }
        var mb = _monitoringButtons[(int)Player.Instance.maxNumStreamers];
        mb.IsOpen = true;

        //슬롯 가격(임시)
        //TODO: 가격책정 다시 해야함
        {
            _monitoringButtons[0].Price = 0;
            _monitoringButtons[1].Price = 0;
            _monitoringButtons[2].Price = 1000;
            _monitoringButtons[3].Price = 50000;
            _monitoringButtons[4].Price = 1000000;
            _monitoringButtons[5].Price = 50000000;
            _monitoringButtons[6].Price = 10000000000;
            _monitoringButtons[7].Price = 100000000000000;

            foreach (var v in _monitoringButtons)
            {
                v.PriceText.text = UnitConversion.ConverseUnit(v.Price).ConversedUnitToString();
                v.Button.transform.Find("Text").GetComponent<Text>().text = "구매 불가";
            }

            _monitoringButtons[Player.Instance.maxNumStreamers].Button.transform.Find("Text").GetComponent<Text>()
                .text = "구매 가능";
        }
        
        //여긴 테스트용임
        Refresh();
        
        CTimeManager.Instance.onOneHourElapse += OnIncreaseMileage;
    }

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < Player.Instance.maxNumStreamers; i++)
        {
            _monitoringButtons[i].SlotText.text = "Empty";
            _monitoringButtons[i].Image.sprite = null;
            _monitoringButtons[i].Image.color = new Color(255, 255, 255, 0);
            _monitoringButtons[i].SlotText.fontSize = 70;
            _monitoringButtons[i].PriceText.text = "";
            _monitoringButtons[i].Streamer = null;
            _monitoringButtons[i].Button.transform.Find("Text").GetComponent<Text>().text = "장착/해제";
            Destroy(_monitoringButtons[i].Button.transform.Find("WealthImg").GetComponent<Image>());
        }
        
        for (int i = 0; i < Player.Instance.equippedStreamers.Length; i++)
        {
            //
            if(Player.Instance.equippedStreamers[i] == null)
                continue;
           
            _monitoringButtons[i].SlotText.text = "이름 : " + Player.Instance.equippedStreamers[i].Tag.ToString() + 
                                                    "\nRank : " + Player.Instance.equippedStreamers[i].Rank.ToString() +
                                                    "\n구독자 수 : " + Player.Instance.equippedStreamers[i].Subscribers;
            _monitoringButtons[i].Streamer = Player.Instance.equippedStreamers[i];
            _monitoringButtons[i].Image.sprite = Resources.Load<Sprite>("CharacterImage/" + Player.Instance.equippedStreamers[i].Tag.ToString());
            _monitoringButtons[i].Image.color = Color.white;
            _monitoringButtons[i].SlotText.transform.localPosition = new Vector3(320,-80,0);
            _monitoringButtons[i].SlotText.fontSize = 35;
        }
    }
        
    private void OnIncreaseMileage()
    {
        var calculatedMileage = CalculateMileage();
        Mileage.Instance.Value += calculatedMileage;
        GameObject.Find("Mileage").GetComponent<CWealthRenderer>().RenderEarnedWealth(calculatedMileage);
    }

    /// <summary>
    /// 획득 마일리지 계산
    /// 각 스트리머의 구독자*등급보정치 의 합 + 각 스트리머의 스킬에 의한 증가치 *(건물주나 사장님 출현시 배수)
    /// </summary>
    public BigInteger CalculateMileage()
    {
        BigInteger calculatedMileage = 0;
        foreach (var v in Player.Instance.equippedStreamers)
        {
            //
            if(v == null)
                continue;
            
            calculatedMileage += (uint)(v.Subscribers * LevelCorrection(v.Rank));
        }

        BigInteger skilledMileage = 0;
        foreach (var v in Player.Instance.equippedStreamers)
        {
            //
            if(v == null)
                continue;
            skilledMileage += v.Skill(calculatedMileage);
        }
        calculatedMileage += skilledMileage;
        
        if (Mileage.Instance.Value + calculatedMileage< 0)
            calculatedMileage = -Mileage.Instance.Value;

        float critical = Random.Range(0f, 1f);
        if (critical <= Player.Instance.probBoss)
            return calculatedMileage * 100;
        if (Player.Instance.probBoss < critical && critical <= Player.Instance.probBoss + Player.Instance.probLord)
            return calculatedMileage * 1000;
        
        return calculatedMileage;
    }
    
    private static float LevelCorrection(ERank rank)
    {
        switch (rank)
        {
            case ERank.A:
                return 1.5f;
            case ERank.B:
                return 1.3f;
            case ERank.C:
                return 1.2f;
            case ERank.D:
                return 1.1f;
            case ERank.E:
                return 1.05f;
            case ERank.F:
                return 1.0f;
        }
        return 0.0f;
    }

    //TODO : 버튼을 넣지말고 addListener에 넣는걸로 바꾸자
    public void ButtonClick(Button button)
    {
        var clickedButton = _monitoringButtons[0];

        int idx = 0;
        foreach (var v in _monitoringButtons)
        {
            if (v.Button.Equals(button))
            {
                clickedButton = v;
                break;
            }
            idx++;
        }

        if (!clickedButton.IsOpen) return;

        //언락버튼 클릭
        if (!clickedButton.IsLocked)
        {
            Debug.Log("Unlock Button Click");
            characterChangerPopupWindow.SetActive(true);
            characterChangerPopupWindow.GetComponent<CCharacterChanger>().clickedButton = clickedButton;
            
            return;
        }

        //락버튼 클릭
        {
            Debug.Log("Lock Button Click");
            
            //돈이 부족할 때
            if (Mileage.Instance.Value < clickedButton.Price)
            {
                var pw = Instantiate(Resources.Load("UIPrefabs/PopUpWindow") as GameObject,transform);
                
                if (pw != null)
                {
                    var text = pw.transform.GetChild(1).Find("Content").gameObject.GetComponent<Text>();
                    pw.transform.SetParent(gameObject.transform.parent.parent);
                    text.text = "마일리지가 부족하군..";
                }
                return;
            }
            //돈이 충분할 때
            if (Mileage.Instance.Value >= clickedButton.Price)
            {
                Mileage.Instance.Value -= clickedButton.Price;
                clickedButton.SlotText.text = "Empty";
                clickedButton.IsLocked = false;
                Player.Instance.maxNumStreamers++;
                if(idx+1 < _monitoringButtons.Length)
                    _monitoringButtons[idx+1].IsOpen = true;
                Refresh();
            }
        }
    }
}
