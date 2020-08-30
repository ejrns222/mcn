using System;
using System.Collections;
using System.Collections.Generic;
using Characters;
using UnityEngine;
using UnityEngine.UI;
using Wealths;

public class CEditSlot : MonoBehaviour
{
   public StreamerBase streamer;
   private float _buttonDownTime = 0;
   private bool _isButtonDown = false;

   private void Start()
   {
      Init();
      //transform.Find("Button").GetComponent<Button>().onClick.AddListener(OnClick);
   }

   private void OnDisable()
   {
      Destroy(gameObject);
   }

   private void Init()
   {
      var increase = (streamer.IncreasingSubs * ((uint) (streamer.AdLevel / 20f) + 1) * (1+(streamer.AdLevel - 20 * (uint) (streamer.AdLevel / 20f) / 2)));
      transform.Find("Name").GetComponent<Text>().text = streamer.Name;
      transform.Find("Desc").GetComponent<Text>().text = "신규 구독자 : " + increase;
      transform.Find("AddLevel").GetComponent<Text>().text = "광고 횟수 : " + streamer.AdLevel;
      transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("CharacterImage/" + streamer.Tag);
      transform.Find("Button").Find("PriceText").GetComponent<Text>().text =
         UnitConversion.ConverseUnit(CalcLevelUpPrice()).ConversedUnitToString();
   }

   private void Update()
   {
      if (_isButtonDown)
      {
         _buttonDownTime += Time.deltaTime;
         if(_buttonDownTime > 0.5f)
            OnClick();
      }
   }

   private long CalcLevelUpPrice()
   {
      return (long) (streamer.AdPrice * Math.Pow(1.12f, (int) streamer.AdLevel));
   }

   public void OnClick()
   {
      _isButtonDown = true;
      var price = CalcLevelUpPrice();
      if (Player.Instance.gold >= price )
      {
         Player.Instance.gold -= price;
         streamer.AdLevel++;
         Init();
         return;
      }
        
      var pw = Instantiate(Resources.Load("UIPrefabs/PopUpWindow") as GameObject,transform.root);
      if (pw != null)
      {
         pw.GetComponent<CPopUpWindow>().SetText("Error:\n현금 잔액 부족");
         _buttonDownTime = -1000;
      }
   }

   public void OnButtonUp()
   {
      _buttonDownTime = 0;
      _isButtonDown = false;
   }
}
