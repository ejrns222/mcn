using UnityEngine;
using UnityEngine.UI;
using Wealths;

public class CDimensionResearch : MonoBehaviour
{
    public static class ResearchSkill
    {
        public static uint DAccelLevel = 0;
        public static uint DBroadLevel = 0;
    }
    
    private Button[] _buttons ;


    private void Awake()
    {
        Load();
        _buttons = GetComponentsInChildren<Button>();
        _buttons[0].onClick.AddListener(DAccelUpgrade);
        _buttons[1].onClick.AddListener(DBroadCastUpgrade);
        Refresh();
        gameObject.SetActive(false);
    }


    private void Refresh()
    {
        _buttons[0].transform.Find("Text").GetComponent<Text>().text = "Lv."+ResearchSkill.DAccelLevel;
        _buttons[1].transform.Find("Text").GetComponent<Text>().text = "Lv."+ResearchSkill.DBroadLevel;

        _buttons[0].transform.Find("priceText").GetComponent<Text>().text = $"{1000 + ResearchSkill.DAccelLevel*200}";
        _buttons[1].transform.Find("priceText").GetComponent<Text>().text = $"{500 + ResearchSkill.DBroadLevel * 100}";

        _buttons[0].transform.parent.Find("Value").GetComponent<Text>().text =
            "시간 흐름 속도 +" + (ResearchSkill.DAccelLevel * 5) + "%";
        _buttons[1].transform.parent.Find("Value").GetComponent<Text>().text =
            "확률 +" + ((float) ResearchSkill.DBroadLevel / 5f).ToString("F1") +"%";
        
        if (ResearchSkill.DAccelLevel == 20)
        {
            _buttons[0].transform.Find("Text").GetComponent<Text>().text = "Lv.Max";
            _buttons[0].transform.Find("priceText").GetComponent<Text>().text = string.Empty;
        }

        if (ResearchSkill.DBroadLevel == 10)
        {
            _buttons[1].transform.Find("Text").GetComponent<Text>().text = "Lv.Max";
            _buttons[1].transform.Find("priceText").GetComponent<Text>().text = string.Empty;
        }
        
        
    }

    private void NotEnoughJewel()
    {
        var pw = Instantiate(Resources.Load("UIPrefabs/PopUpWindow") as GameObject,transform.root);
                
        if (pw != null)
        {
            pw.GetComponent<CPopUpWindow>().SetText("Error:\n차원석 부족");
        }
    }
    
    private void DAccelUpgrade()
    {
        if (ResearchSkill.DAccelLevel >= 20)
        {
            return;
        }

        long price = 1000 + ResearchSkill.DAccelLevel * 200;
        if (Player.Instance.jewel < price)
        {
            NotEnoughJewel();
            return;
        }

        Player.Instance.jewel -= price;
        ResearchSkill.DAccelLevel++;
        Refresh();
        
        if(ResearchSkill.DAccelLevel == 20)
            Destroy(_buttons[0].transform.Find("WealthImg").GetComponent<Image>());
        
    }

    private void DBroadCastUpgrade()
    {
        if (ResearchSkill.DBroadLevel >= 10)
        {
            return;
        }
        
        long price = 500 + ResearchSkill.DBroadLevel * 100;
        if (Player.Instance.jewel < price)
        {
            NotEnoughJewel();
            return;
        }

        Player.Instance.jewel -= price;
        ResearchSkill.DBroadLevel++;
        Refresh();
        
        if (ResearchSkill.DBroadLevel == 10)
            Destroy(_buttons[1].transform.Find("WealthImg").GetComponent<Image>());
    }

    //ResearchSkill저장
    public static void Save()
    {
        uint[] skillArray = new uint[2]{ResearchSkill.DAccelLevel,ResearchSkill.DBroadLevel};
        CSaveLoadManager.CreateJsonFileForArray(skillArray,"SaveFiles","ResearchSkill");
    }

    //ResearchSkill로드
    private void Load()
    {
        uint[] skillArray = CSaveLoadManager.LoadJsonFileToArray<uint>("SaveFiles","ResearchSkill");
        if (skillArray == null)
            return;
        ResearchSkill.DAccelLevel = skillArray[0];
        ResearchSkill.DBroadLevel = skillArray[1];
    }
}
