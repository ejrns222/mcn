using System;
using System.Collections;
using Characters;
using UnityEngine;
using UnityEngine.UI;
using Wealths;
using Random = UnityEngine.Random;

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
        private float _rndTime;
        private float _deltaTime;
        
        private void Awake()
        {
            transform.Find("Button").GetComponent<Button>().onClick.AddListener(ButtonClick);
            ChangeState(slotState);
            _rndTime = Random.Range(0.5f, 3f);
        }

        private void Update()
        {
            if (streamer == null)
                return;
            _deltaTime += Time.deltaTime;
            if (_deltaTime >= _rndTime)
            {
                StartCoroutine(Donation());
                _deltaTime = 0;
                _rndTime = Random.Range(0.5f, 3f);
            }
        }

        private IEnumerator Donation()
        {
            var text = transform.Find("Display/DonationText").GetComponent<Text>();
            Color color = new Color(1,1,0,0);
            text.color = color;

            int donation = Random.Range(1000, (int)streamer.Expectation);
            donation -= donation % 1000;
            text.text = donation + "원 후원!!";
            
            while (text.color.a < 1)
            {
                color.a += 0.05f;
                text.color = color;
                yield return new WaitForSeconds(0.02f); 
            }
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
            Image streamerImg = transform.Find("Display/StreamerImg").GetComponent<Image>();
            Image monitorImg = transform.Find("Display/Monitor").GetComponent<Image>();
            Image wealtImg = transform.Find("Button").Find("WealthImg").GetComponent<Image>();

            switch (state)
            {
                case SlotState.OpenEmpty:
                    nameText.text = "Empty";
                    nameText.transform.localPosition = new Vector3(250,-45,0);
                    nameText.fontSize = 70;
                    nameText.color= new Color(0.7058f, 0.7058f, 0.7058f, 1f);
                    descText.text = string.Empty;
                    priceText.text = string.Empty;
                    transform.Find("Button").gameObject.SetActive(true);
                    buttonText.text = "교육시작";
                    //streamerImg.sprite = null;
                    //streamerImg.gameObject.SetActive(false);
                    streamerImg.transform.parent.gameObject.SetActive(true);
                    monitorImg.sprite = Resources.Load<Sprite>("UIImage/MonitoringNoSignal");
                    streamer = null;
                    wealtImg.gameObject.SetActive(false);
                    break;
                case SlotState.OpenEquipped:
                    streamer = Player.Instance.equippedStreamers[index];
                    nameText.text = streamer.Name;
                    nameText.transform.localPosition = new Vector3(200, -30,0);
                    nameText.fontSize = 50;
                    nameText.color= new Color(0.7058f, 0.7058f, 0.7058f, 1f);
                    descText.text = "Rank : " + streamer.Rank +
                                    "\n구독자 수 : " + streamer.Subscribers;
                    descText.transform.localPosition = new Vector3(200, -80,0);
                    buttonText.text = "교체/해제";
                    transform.Find("Button").gameObject.SetActive(true);
                    descText.fontSize = 30;
                    priceText.text = string.Empty;
                    streamerImg.sprite = Resources.Load<Sprite>("CharacterImage/" + streamer.Tag);
                    //streamerImg.color = Color.white;
                    //streamerImg.gameObject.SetActive(true);
                    streamerImg.transform.parent.gameObject.SetActive(true);
                    monitorImg.sprite = Resources.Load<Sprite>("UIImage/MonitoringBG");
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
                    //streamerImg.sprite = null;
                    //streamerImg.gameObject.SetActive(false);
                    streamerImg.transform.parent.gameObject.SetActive(false);
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
                    //streamerImg.sprite = null;
                    //streamerImg.gameObject.SetActive(false);
                    streamerImg.transform.parent.gameObject.SetActive(false);
                    streamer = null;
                    break;
            }
        }

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
                        pw.transform.Find("Panel").Find("Content").GetComponent<Text>().text = "Error:\n마일리지 부족";
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