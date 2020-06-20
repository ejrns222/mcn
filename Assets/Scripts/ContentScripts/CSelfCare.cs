using System;
using UnityEngine;
using UnityEngine.UI;
using Wealths;

public class CSelfCare : MonoBehaviour
{
    public struct Skill
    {
        public uint EditLevel ;
        public uint BotLevel;
        public uint SocialLevel;
        public  uint ComputerLevel;
        public uint BroadCastLevel;
    }
    
    private Button[] _buttons;
    public static Skill CareSkill;
    
    
    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>();

        {
            _buttons[0].onClick.AddListener(VideoEditStudy);
            _buttons[1].onClick.AddListener(BotUpgrade);
            _buttons[2].onClick.AddListener(SocialLevelUp);
            _buttons[3].onClick.AddListener(ComUpgrade);
            _buttons[4].onClick.AddListener(BroadCastUp);
        }

        Load();
        
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        _buttons[0].transform.Find("Text").GetComponent<Text>().text = "Lv." + CareSkill.EditLevel.ToString();
        _buttons[1].transform.Find("Text").GetComponent<Text>().text = "Lv." + CareSkill.BotLevel.ToString();
        _buttons[2].transform.Find("Text").GetComponent<Text>().text = "Lv." + CareSkill.SocialLevel.ToString();
        _buttons[3].transform.Find("Text").GetComponent<Text>().text = "Lv." + CareSkill.ComputerLevel.ToString();
        _buttons[4].transform.Find("Text").GetComponent<Text>().text = "Lv." + CareSkill.BroadCastLevel.ToString();
        
        _buttons[0].transform.Find("priceText").GetComponent<Text>().text = UnitConversion.ConverseUnit((long)(10000 * Math.Pow(1.12f,(int)CareSkill.EditLevel))).ConversedUnitToString();
        _buttons[1].transform.Find("priceText").GetComponent<Text>().text = UnitConversion.ConverseUnit((long)(10000 * Math.Pow(1.12f,(int)CareSkill.BotLevel))).ConversedUnitToString();
        _buttons[2].transform.Find("priceText").GetComponent<Text>().text = UnitConversion.ConverseUnit((long)(10000 * Math.Pow(1.12f,(int)CareSkill.SocialLevel))).ConversedUnitToString();
        _buttons[3].transform.Find("priceText").GetComponent<Text>().text = UnitConversion.ConverseUnit((long)(100000 * Math.Pow(1.12f,(int)CareSkill.ComputerLevel))).ConversedUnitToString();
        _buttons[4].transform.Find("priceText").GetComponent<Text>().text = UnitConversion.ConverseUnit((long)(500000 * Math.Pow(10.5f,(int)CareSkill.BroadCastLevel))).ConversedUnitToString();

        _buttons[0].transform.parent.Find("Value").GetComponent<Text>().text =
            "+" + (10 * ((uint) (CareSkill.EditLevel / 50f) + 1) *
             (CareSkill.EditLevel - 50 * (uint) (CareSkill.EditLevel / 50f) / 2)) + "%";
        _buttons[1].transform.parent.Find("Value").GetComponent<Text>().text =
            "+" + (10 * ((uint) (CareSkill.BotLevel / 50f) + 1) *
                  (CareSkill.BotLevel - 50 * (uint) (CareSkill.BotLevel / 50f) / 2)) + "%";
        _buttons[2].transform.parent.Find("Value").GetComponent<Text>().text =
            "+" + $"{CareSkill.SocialLevel * 0.2f:0.#}" + "%";
        _buttons[3].transform.parent.Find("Value").GetComponent<Text>().text = CareSkill.ComputerLevel.ToString();
        _buttons[4].transform.parent.Find("Value").GetComponent<Text>().text = CareSkill.BroadCastLevel.ToString();
        

        //TODO:레벨 맥스 되면 버튼 어둡게 + 텍스트 레벨맥스로, 가격과 가격 이미지 지우기
        if (CareSkill.SocialLevel >= 100)
        {
            _buttons[2].transform.Find("Text").GetComponent<Text>().text = "Lv.Max";
            _buttons[2].transform.Find("priceText").GetComponent<Text>().text =String.Empty;
            Destroy(_buttons[2].transform.Find("WealthImg").GetComponent<Image>());
        }
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
        long price = (long)(10000 * Math.Pow(1.12f,(int)CareSkill.EditLevel));
        if (Player.Instance.gold < price)
        {
            NotEnoughGold();
            return;
        }

        Player.Instance.gold -= price;
        CareSkill.EditLevel++;
        Init();
    }

    private void ComUpgrade()
    {
        long price = (long)(100000 * Math.Pow(1.12f,(int)CareSkill.ComputerLevel));
        if (Player.Instance.gold < price)
        {
            NotEnoughGold();
            return;
        }

        Player.Instance.gold -= price;
        CareSkill.ComputerLevel++;
        Init();
    }
    
    private void BotUpgrade()
    {
        long price = (long)(10000 * Math.Pow(1.12f,(int)CareSkill.BotLevel));
        if (Player.Instance.gold < price)
        {
            NotEnoughGold();
            return;
        }

        Player.Instance.gold -= price;
        CareSkill.BotLevel++;
        Init();
    }

    private void SocialLevelUp()
    {
        if (CareSkill.SocialLevel >= 100)
        {
            Init();
            return;
        }
        long price = (long)(10000 * Math.Pow(1.12f,(int)CareSkill.SocialLevel));
        if (Player.Instance.gold < price)
        {
            NotEnoughGold();
            return;
        }

        Player.Instance.gold -= price;
        CareSkill.SocialLevel++;
        Init();
    }

    private void BroadCastUp()
    {
        long price = (long)(500000 * Math.Pow(1.5f,(int)CareSkill.BroadCastLevel));
        if (Player.Instance.gold < price)
        {
            NotEnoughGold();
            return;
        }

        Player.Instance.gold -= price;
        CareSkill.BroadCastLevel++;
        Init();
    }

    public static void Save()
    {
        uint[] skillArray = new uint[5]{CareSkill.EditLevel,CareSkill.BotLevel,CareSkill.SocialLevel,CareSkill.ComputerLevel,CareSkill.BroadCastLevel};
        CSaveLoadManager.CreateJsonFileForArray(skillArray,"SaveFiles","CareSkill");
    }

    private void Load()
    {
        uint[] skillArray = CSaveLoadManager.LoadJsonFileToArray<uint>("SaveFiles","CareSkill");
        if (skillArray == null)
        {
            CareSkill.EditLevel = 0;
            CareSkill.BotLevel = 0;
            CareSkill.SocialLevel = 0;
            CareSkill.ComputerLevel = 0;
            CareSkill.BroadCastLevel = 0;
            return;
        }
        CareSkill.EditLevel = skillArray[0];
        CareSkill.BotLevel = skillArray[1];
        CareSkill.SocialLevel = skillArray[2];
        CareSkill.ComputerLevel = skillArray[3];
        CareSkill.BroadCastLevel = skillArray[4];
    }
}
