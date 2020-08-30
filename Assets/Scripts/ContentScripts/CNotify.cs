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

    [SerializeField] private GameObject basicNotify = null;
    [SerializeField] private GameObject dimensionNotify = null;
    [SerializeField] private GameObject membershipLvUp = null;

    private void Awake()
    {
        Load();
        
        basicNotify.transform.Find("Button").GetComponent<Button>().onClick.AddListener(OnBasicNotifyButton);
        dimensionNotify.transform.Find("Button").GetComponent<Button>().onClick.AddListener(OnDimensionButton);
        membershipLvUp.transform.Find("Button").GetComponent<Button>().onClick.AddListener(OnMembershipLvUpButton);
        RefreshMembership();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Save()
    {
        uint[] array = new uint[1]{MembershipLv};
        
        CSaveLoadManager.CreateJsonFileForArray(array,"SaveFiles","Notify");
    }

    private void Load()
    { 
        var array = CSaveLoadManager.LoadJsonFileToArray<uint>("SaveFiles", "Notify");
        if (array == null)
            return;
        MembershipLv = array[0];
    }

    private void OnBasicNotifyButton()
    {
        if (Player.Instance.gold < 1000000)
        {
            var pw = Instantiate(Resources.Load("UIPrefabs/PopUpWindow") as GameObject,transform.root);
                
            if (pw != null)
            {
                pw.GetComponent<CPopUpWindow>().SetText("Error:\n골드 부족");
            }
            return;
        }

        Player.Instance.gold -= 1000000;
        CBuffManager.NumProbCDE += 3;
    }

    private void OnDimensionButton()
    {
        if (Player.Instance.jewel < 500)
        {
            var pw = Instantiate(Resources.Load("UIPrefabs/PopUpWindow") as GameObject,transform.root);
                
            if (pw != null)
            {
                pw.GetComponent<CPopUpWindow>().SetText("Error:\n차원석 부족");
            }
            return;
        }

        Player.Instance.jewel -= 500;
        CBuffManager.NumProbAB+=3;
    }

    private void OnMembershipLvUpButton()
    {
        long price = 50000 * (long) Math.Pow(30, MembershipLv - 1);
        if (Player.Instance.mileage < price)
        {
            var pw = Instantiate(Resources.Load("UIPrefabs/PopUpWindow") as GameObject,transform.root);
                
            if (pw != null)
            {
                pw.GetComponent<CPopUpWindow>().SetText("Error:\n마일리지 부족");
            }
            return;
        }

        if ((int)MembershipLv >= 5)
            return;

        Player.Instance.mileage -= price;
        MembershipLv++;
        Save();
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
