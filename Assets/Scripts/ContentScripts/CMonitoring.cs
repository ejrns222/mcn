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
    public Text ButtonText;
    public Text PriceText;
    public IStreamer Streamer;
    public bool IsLocked;
    public bool IsOpen;
    public BigInteger Price;
}

public class CMonitoring : MonoBehaviour
{
    //private List<Text> _buttonTexts;
    private List<MonitoringButton> _monitoringButtons;
    public GameObject characterChangerPopupWindow;

    private void Awake()
    {
        _monitoringButtons = new List<MonitoringButton>(8);

        var buttons = transform.GetChild(0).GetChild(0).GetChild(0);
        for (int i = 0; i < buttons.childCount; i++)
        {
            var v = buttons.GetChild(i).GetComponent<Button>();
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
            monitoringButton.ButtonText.text = "Empty";
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
                v.PriceText.text = "필요 마일리지 : " + v.Price.ToString();
            }
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
            _monitoringButtons[i].ButtonText.text = "Empty";
            _monitoringButtons[i].Image.sprite = null;
            _monitoringButtons[i].Image.color = new Color(255, 255, 255, 0);
            _monitoringButtons[i].ButtonText.fontSize = 70;
            _monitoringButtons[i].PriceText.text = "";
        }
        
        for (int i = 0; i < Player.Instance.equippedStreamers.Count; i++)
        {
            _monitoringButtons[i].ButtonText.text = "이름 : " + Player.Instance.equippedStreamers[i].Tag.ToString() + 
                                                    "\nRank : " + Player.Instance.equippedStreamers[i].Rank.ToString() +
                                                    "\n구독자 수 : " + Player.Instance.equippedStreamers[i].Subscribers;
            _monitoringButtons[i].Streamer = Player.Instance.equippedStreamers[i];
            _monitoringButtons[i].Image.sprite = Resources.Load<Sprite>("CharacterImage/" + Player.Instance.equippedStreamers[i].Tag.ToString());
            _monitoringButtons[i].Image.color = Color.white;
            _monitoringButtons[i].ButtonText.transform.localPosition = new Vector3(360,-80,0);
            _monitoringButtons[i].ButtonText.fontSize = 35;
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
            calculatedMileage += (uint)(v.Subscribers * LevelCorrection(v.Rank));
        }

        BigInteger skilledMileage = 0;
        foreach (var v in Player.Instance.equippedStreamers)
        {
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
                clickedButton.ButtonText.text = "Empty";
                clickedButton.IsLocked = false;
                Player.Instance.maxNumStreamers++;
                if(idx+1 < _monitoringButtons.Count)
                    _monitoringButtons[idx+1].IsOpen = true;
            }
        }
    }
}
