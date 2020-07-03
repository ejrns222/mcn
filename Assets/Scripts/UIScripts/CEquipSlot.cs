using System;
using Characters;
using UnityEngine;
using UnityEngine.UI;
using Wealths;

namespace UIScripts
{
    public enum SlotState
    {
        OpenEmpty,
        OpenEquipped,
        Locked,
        Disabled,
    }
    public class CEquipSlot : MonoBehaviour
    {
        public StreamerBase streamer;
        
        
        [SerializeField] public SlotState slotState;
        [SerializeField] private long price = 0;
        [SerializeField] private CCharacterChanger characterChanger = null;
        [SerializeField] public uint index;

        private void Awake()
        {
            transform.Find("Button").GetComponent<Button>().onClick.AddListener(ButtonClick);
            ChangeState(slotState);
        }

        public void Refresh()
        {
            if (slotState == SlotState.OpenEquipped)
            {
                streamer = Player.Instance.equippedStreamers[index];
                Text descText = transform.Find("Desc").GetComponent<Text>();
                descText.text = "Rank : " + streamer.Rank +
                                "\n구독자 수 : " + streamer.Subscribers;
            }
        }

        public void ChangeState(SlotState state)
        {
            slotState = state;
            Text nameText = transform.Find("Name").GetComponent<Text>();
            Text descText = transform.Find("Desc").GetComponent<Text>();
            Text priceText = transform.Find("Button").Find("priceText").GetComponent<Text>();
            Text buttonText = transform.Find("Button").Find("Text").GetComponent<Text>();
            Image streamerImg = transform.Find("StreamerImg").GetComponent<Image>();
            Image wealtImg = transform.Find("Button").Find("WealthImg").GetComponent<Image>();

            switch (state)
            {
                case SlotState.OpenEmpty:
                    nameText.text = "Empty";
                    nameText.transform.localPosition = new Vector3(200,-45,0);
                    nameText.fontSize = 70;
                    nameText.color= new Color(0.7058f, 0.7058f, 0.7058f, 1f);
                    descText.text = string.Empty;
                    priceText.text = string.Empty;
                    transform.Find("Button").gameObject.SetActive(true);
                    buttonText.text = "장착/해제";
                    streamerImg.sprite = null;
                    streamerImg.gameObject.SetActive(false);
                    streamer = null;
                    wealtImg.gameObject.SetActive(false);
                    break;
                case SlotState.OpenEquipped:
                    streamer = Player.Instance.equippedStreamers[index];
                    nameText.text = streamer.Tag.ToString();
                    nameText.transform.localPosition = new Vector3(130, -15,0);
                    nameText.fontSize = 60;
                    nameText.color= new Color(0.7058f, 0.7058f, 0.7058f, 1f);
                    descText.text = "Rank : " + streamer.Rank +
                                    "\n구독자 수 : " + streamer.Subscribers;
                    descText.transform.localPosition = new Vector3(130, -80,0);
                    buttonText.text = "장착/해제";
                    transform.Find("Button").gameObject.SetActive(true);
                    descText.fontSize = 30;
                    priceText.text = string.Empty;
                    streamerImg.sprite = Resources.Load<Sprite>("CharacterImage/" + streamer.Tag);
                    streamerImg.color = Color.white;
                    streamerImg.gameObject.SetActive(true);
                    wealtImg.gameObject.SetActive(false);
                    break;
                case SlotState.Locked:
                    nameText.text = "Lock";
                    transform.Find("Button").gameObject.SetActive(true);
                    wealtImg.gameObject.SetActive(true);
                    nameText.transform.localPosition = new Vector3(200,-45,0);
                    nameText.fontSize = 70;
                    nameText.color= new Color(0.7058f, 0.7058f, 0.7058f, 1f);
                    descText.text = string.Empty;
                    priceText.text = UnitConversion.ConverseUnit(price).ConversedUnitToString();
                    buttonText.text = "구매";
                    streamerImg.sprite = null;
                    streamerImg.gameObject.SetActive(false);
                    streamer = null;
                    break;
                case SlotState.Disabled:
                    transform.Find("Button").gameObject.SetActive(false);
                    nameText.text = "Disable";
                    nameText.transform.localPosition = new Vector3(200,-45,0);
                    nameText.fontSize = 70;
                    nameText.color= new Color(0.3f,0.3f,0.3f,1);
                    descText.text = string.Empty;
                    priceText.text = string.Empty;
                    buttonText.text = string.Empty;
                    streamerImg.sprite = null;
                    streamerImg.gameObject.SetActive(false);
                    streamer = null;
                    break;
            }
        }

        //TODO: 유틸에 팝업윈도우나 대화창 등을 띄우는 스태틱클래스를 하나 만들자.
        private void ButtonClick()
        {
            switch (slotState)
            {
                case SlotState.OpenEmpty:
                case SlotState.OpenEquipped:
                    characterChanger.gameObject.SetActive(true);
                    characterChanger.selectedEquipSlot = this;
                    break;
                case SlotState.Locked:
                    if (Player.Instance.mileage < price)
                    {
                        var pw = Instantiate(Resources.Load("UIPrefabs/PopUpWindow") as GameObject,transform.root);
                        pw.transform.Find("Panel").Find("Content").GetComponent<Text>().text = "마일리지가 부족해..";
                        break;
                    }

                    Player.Instance.mileage -= price;
                    ChangeState(SlotState.OpenEmpty);
                    
                    var equipSlots = transform.parent.GetComponentsInChildren<CEquipSlot>();
                    foreach (var v in equipSlots)
                    {
                        if (v.slotState == SlotState.Disabled)
                        {
                            v.ChangeState(SlotState.Locked);
                            break;
                        }
                    }
                    break;
                case SlotState.Disabled:
                    break;
            }
        }
    }
}