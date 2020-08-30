using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShop : MonoBehaviour
{
   private void Start()
   {
      gameObject.SetActive(false);
   }

   public void PurchaseJewel(int num)
   {
      Player.Instance.jewel += num;
   }

   public void PurchaseMileage(int num)
   {
      Player.Instance.mileage += num;
   }

   public void PurchaseGold(int num)
   {
      Player.Instance.gold += num;
   }

   public void NintendoPLZ()
   {
      var pw = Instantiate(Resources.Load("UIPrefabs/PopUpWindow") as GameObject,transform.root);
      if (pw != null)
      {
         pw.GetComponent<CPopUpWindow>().SetText("010-8365-3654 어디든 달려가겠습니다. 연락주십쇼. 닫으면 다시 열 수 없습니다");
      }
         
   }
}
