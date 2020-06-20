using System;
using UnityEngine;
using UnityEngine.UI;
using Wealths;

/// <summary>
/// @brief : 채용공고 컨텐츠
/// </summary>
public class CNotify : MonoBehaviour
{
    public static uint MembershipLv = 1;
    public static bool IsBasicNotify;
    public static bool IsDimensionNotify;

    private DateTime _basicEndTime;
    private DateTime _dimensionEndTime;

    [SerializeField] private GameObject basicNotify = null;
    [SerializeField] private GameObject dimensionNotify = null;
    [SerializeField] private GameObject membershipLvUp = null;

    private void Awake()
    {
        Load();
        
        basicNotify.transform.Find("Button").GetComponent<Button>().onClick.AddListener(OnBasicNotifyButton);
        dimensionNotify.transform.Find("Button").GetComponent<Button>().onClick.AddListener(OnDimensionButton);
        membershipLvUp.transform.Find("Button").GetComponent<Button>().onClick.AddListener(OnMembershipLvUpButton);
        IsBasicNotify = false;
        IsDimensionNotify = false;
        RefreshMembership();
    }

    private void FixedUpdate()
    {
        if (IsBasicNotify)
        {
            var remainTime = _basicEndTime - DateTime.Now;
            if(remainTime.Days > 0)
                basicNotify.transform.Find("Panel").Find("TimeText").GetComponent<Text>().text =
                "남은 시간: " + remainTime.Days + "일 " + remainTime.Hours + "시간";
            else
                basicNotify.transform.Find("Panel").Find("TimeText").GetComponent<Text>().text =
                    "남은 시간: "  + remainTime.Hours + "시간";

            if (remainTime <= TimeSpan.Zero)
            {
                IsBasicNotify = true;
                basicNotify.transform.Find("Panel").Find("TimeText").GetComponent<Text>().text = string.Empty;
            }
        }

        if (IsDimensionNotify)
        {
            var remainTime = _dimensionEndTime - DateTime.Now;
            if(remainTime.Days > 0)
                dimensionNotify.transform.Find("Panel").Find("TimeText").GetComponent<Text>().text =
                    "남은 시간: " + remainTime.Days + "일 " + remainTime.Hours + "시간";
            else
                dimensionNotify.transform.Find("Panel").Find("TimeText").GetComponent<Text>().text =
                    "남은 시간: "  + remainTime.Hours + "시간";

            if (remainTime <= TimeSpan.Zero)
            {
                IsDimensionNotify = true;
                dimensionNotify.transform.Find("Panel").Find("TimeText").GetComponent<Text>().text = string.Empty;
            }
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Save()
    {
        //위에 스태틱 3개 저장
    }

    private void Load()
    {
        //스태틱 3개 로드
    }

    private void OnBasicNotifyButton()
    {
        if (Player.Instance.gold < 1000000)
        {
            var pw = Instantiate(Resources.Load("UIPrefabs/PopUpWindow") as GameObject,transform.root);
                
            if (pw != null)
            {
                pw.GetComponent<CPopUpWindow>().SetText("돈이 부족하군..");
            }
            return;
        }

        Player.Instance.gold -= 1000000;
        if (!IsBasicNotify)
        {
            IsBasicNotify = true;
            _basicEndTime = DateTime.Now.AddHours(6);
            return;
        }

        _basicEndTime = _basicEndTime.AddHours(6);
    }

    private void OnDimensionButton()
    {
        if (Player.Instance.jewel < 500)
        {
            var pw = Instantiate(Resources.Load("UIPrefabs/PopUpWindow") as GameObject,transform.root);
                
            if (pw != null)
            {
                pw.GetComponent<CPopUpWindow>().SetText("차원석이 부족하군..");
            }
            return;
        }

        Player.Instance.jewel -= 500;
        if (!IsDimensionNotify)
        {
            IsDimensionNotify = true;
            _dimensionEndTime = DateTime.Now.AddHours(6);
            return;
        }

        _dimensionEndTime = _dimensionEndTime.AddHours(6);
    }

    private void OnMembershipLvUpButton()
    {
        long price = 50000 * (long) Math.Pow(30, MembershipLv - 1);
        if (Player.Instance.mileage < price)
        {
            var pw = Instantiate(Resources.Load("UIPrefabs/PopUpWindow") as GameObject,transform.root);
                
            if (pw != null)
            {
                pw.GetComponent<CPopUpWindow>().SetText("마일리지가 부족하군..");
            }
            return;
        }

        if ((int)MembershipLv >= 5)
            return;

        Player.Instance.mileage -= price;
        MembershipLv++;
        GameObject.Find("Recruit").GetComponent<CRecruit>().Refresh();
        RefreshMembership();
    }

    private void RefreshMembership()
    {
        long price = 50000 * (long) Math.Pow(30, MembershipLv - 1);
        membershipLvUp.transform.Find("Panel").Find("TimeText").GetComponent<Text>().text = "Lv." + MembershipLv;
        membershipLvUp.transform.Find("Button").Find("price").GetComponent<Text>().text =
            UnitConversion.ConverseUnit(price).ConversedUnitToString();
        if (MembershipLv == 5)
        {
            membershipLvUp.transform.Find("Button").Find("Text").GetComponent<Text>().text = "Lv.MAX";
            membershipLvUp.transform.Find("Button").Find("Text").GetComponent<Text>().fontSize = 60;
            membershipLvUp.transform.Find("Button").Find("price").GetComponent<Text>().text = string.Empty;
            membershipLvUp.transform.Find("Button").Find("Image").gameObject.SetActive(false);
            return;
        }
        membershipLvUp.transform.Find("Button").Find("Text").GetComponent<Text>().text =
            "Lv." + MembershipLv + "->Lv." + (MembershipLv + 1);
    }
}
