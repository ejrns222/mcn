using System;
using Characters;
using UnityEngine;
using UnityEngine.UI;
using Util;
using Wealths;
using Random = UnityEngine.Random;

public class CRecruit : MonoBehaviour
{
   public GameObject popupWindowPrefab;
   public GameObject summonWindowPrefab;
   private Button[] _summonButtons;

   private int[] _probGeneralSummon;
   private int[] _probSpecialSummon;
   private int[] _probFreeSummon;

   private static uint _numNewRecruit = 0;
   private static uint _numExpertRecruit = 0;
   private static uint _numFreeRecruit = 0;
   private const long NewRecruitPrice = 20000;
   private const long ExpertRecruitPrice = 300;

   private void Awake()
   {
      _summonButtons = transform.Find("Content").GetComponentsInChildren<Button>();
      _probGeneralSummon = new int[5]{10,30,70,150,200};
      _probSpecialSummon = new int[5]{20,50,100,200,250};
      _probFreeSummon = new int[5] {0, 0, 30, 50, 100};
      Load();
      Refresh();
   }

   public void Refresh()
   {
      _summonButtons[2].GetComponentInChildren<Text>().text =
         "무료 " + (CNotify.MembershipLv - _numFreeRecruit) + "/" + CNotify.MembershipLv;
      
      long price = (long) (NewRecruitPrice * Math.Pow(1.3d, _numNewRecruit));
      _summonButtons[0].GetComponentInChildren<Text>().text =
         UnitConversion.ConverseUnit(price).ConversedUnitToString();
      
      price = (long) (ExpertRecruitPrice * Math.Pow(1.3d, _numExpertRecruit));
      _summonButtons[1].GetComponentInChildren<Text>().text =
         UnitConversion.ConverseUnit(price).ConversedUnitToString();
   }

   /// <summary>
   /// @brief : 일반적 채용, 뽑을때마다 가격이 30% 증가
   /// </summary>
   public void GeneralSummon()
   {
      long price = (long) (NewRecruitPrice * Math.Pow(1.3d, _numNewRecruit));
      if (Player.Instance.mileage < price)
      {
         Instantiate(popupWindowPrefab, transform.root).GetComponent<CPopUpWindow>().SetText("Error:\n마일리지 부족");
         return;
      }
      else
      {
         var gatchaResult = StreamerGatcha(_probGeneralSummon);
         Payment(gatchaResult, EWealth.Mileage, price,false);
         

            _numNewRecruit++;
            price = (long) (NewRecruitPrice * Math.Pow(1.3d, _numNewRecruit));
            _summonButtons[0].GetComponentInChildren<Text>().text =
               UnitConversion.ConverseUnit(price).ConversedUnitToString();
      }
   }

   /// <summary>
   /// @brief : 특별 채용, 뽑을때마다 가격이 30% 증가
   /// </summary>
   public void SpecialSummon()
   {
      long price = (long) (ExpertRecruitPrice * Math.Pow(1.3d, _numExpertRecruit));
      if (Player.Instance.jewel < price)
      {
         Instantiate(popupWindowPrefab, transform.root).GetComponent<CPopUpWindow>().SetText("Error:\n차원석 부족");
         return;
      }
      else
      {
         var gatchaResult = StreamerGatcha(_probSpecialSummon);  
         Payment(gatchaResult, EWealth.Jewel, price,true);

         _numExpertRecruit++;
         price = (long) (ExpertRecruitPrice * Math.Pow(1.3d, _numExpertRecruit));
         _summonButtons[1].GetComponentInChildren<Text>().text =
            UnitConversion.ConverseUnit(price).ConversedUnitToString();
      }
   }

   public void FreeSummon()
   {
      if (CNotify.MembershipLv - _numFreeRecruit == 0)
      {
         Instantiate(popupWindowPrefab, transform.root).GetComponent<CPopUpWindow>().SetText("Error:\n무료횟수 없음");
         return;
      }
      //TODO:광고출력
      var gatchaResult = StreamerGatcha(_probGeneralSummon);
      Payment(gatchaResult, EWealth.Mileage, 0,false);

      _numFreeRecruit++;
      _summonButtons[2].GetComponentInChildren<Text>().text =
         "무료 " + (CNotify.MembershipLv - _numFreeRecruit) + "/" + CNotify.MembershipLv;
   }

   /// <summary>
   /// 누적 가중치 랜덤 방식으로 도감에서 랜덤한 스트리머를 가져온다
   /// switch문이 좀 더러워 보이지만 매번 allStreamer에서 랭크로 비교해서 찾으면 느릴것같음.
   /// </summary>
   /// <param name="prob"> 크기5의 확률을 나타내는 배열 1000 = 100%</param>
   /// <returns></returns>
   private StreamerBase StreamerGatcha(int[] prob)
   {
      var getProb = Random.Range(1, 1001);
            int cumulative = 0;
            ERank? result = null;
            //누적 가중치 랜덤
            for (int i = 0; i < prob.Length + 1; i++)
            {
               if (i < prob.Length)
               {
                  if (CNotify.IsDimensionNotify && i < 2)
                     cumulative += prob[i] + 5;
                  else if (CNotify.IsBasicNotify && i >= 2)
                     cumulative += prob[i] + 10;
                  else
                     cumulative += prob[i];
                  
                  Debug.Log(cumulative);
               }
               else
                  cumulative = 1000;

               if (getProb <= cumulative)
               {
                  result = (ERank) i;
                  break;
               }
            }

            StreamerBase gatchaResult = null;
            int num;
            switch (result)
            {
               case ERank.A:
                  if (CDictionary.StreamersA.Count != 0)
                  {
                     num = Random.Range(0, CDictionary.StreamersA.Count);
                     gatchaResult = CDictionary.StreamersA[num];
                  }
                  break;
               case ERank.B:
                  if (CDictionary.StreamersB.Count != 0)
                  {
                     num = Random.Range(0, CDictionary.StreamersB.Count);
                     gatchaResult = CDictionary.StreamersB[num];
                  }
                  break;
               case ERank.C:
                  if (CDictionary.StreamersC.Count != 0)
                  {
                     num = Random.Range(0, CDictionary.StreamersC.Count);
                     gatchaResult = CDictionary.StreamersC[num];
                  }
                  break;
               case ERank.D:
                  if (CDictionary.StreamersD.Count != 0)
                  {
                     num = Random.Range(0, CDictionary.StreamersD.Count);
                     gatchaResult = CDictionary.StreamersD[num];
                  }
                  break;
               case ERank.E:
                  if (CDictionary.StreamersE.Count != 0)
                  {
                     num = Random.Range(0, CDictionary.StreamersE.Count);
                     gatchaResult = CDictionary.StreamersE[num];
                  }
                  break;
               case ERank.F:
                  if (CDictionary.StreamersF.Count != 0)
                  {
                     num = Random.Range(0, CDictionary.StreamersF.Count);
                     gatchaResult = CDictionary.StreamersF[num];
                  }
                  break;
               default:
                  Debug.Log("가챠 에러");
                  return null;
            }

            if (gatchaResult == null)
            {
               Debug.Log("아직 없는 안만들어진 랭크");
               return null;
            }

            foreach (var v in CDictionary.AllStreamers)
            {
               if (v.Key.Tag == gatchaResult.Tag)
               {
                  CDictionary.AllStreamers[v.Key] = true;
                  break;
               }
            }
            return gatchaResult;
   }

   /// <summary>
   /// @brief : 뽑은 스트리머를 복사해서 인벤토리에 넣고 돈을 지불한다.
   /// 좀 지저분하긴 한데 걍 쓰자
   /// </summary>
   /// <param name="receivedStreamer"> 뽑은 스트리머</param>
   /// <param name="eWealth"> 지불 할 재화 종류</param>
   /// <param name="price"> 가격</param>
   /// <param name="isExpert">특별채용이면 true</param>
   private void Payment(StreamerBase receivedStreamer, EWealth eWealth, long price, bool isExpert)
   {
      if (receivedStreamer == null)
      {
         Debug.Log("스트리머가 없음");
         return;
      }
      var clone = (StreamerBase) Activator.CreateInstance(receivedStreamer.GetType());
      if (isExpert)
      {
         clone.Subscribers = clone.Expectation / 2;
         clone.AdLevel = 3;
      }

      CInventory.streamerList.Add(clone);
         
      switch (eWealth)
      {
         case EWealth.Mileage:
            Player.Instance.mileage -= price;
            break;
         case EWealth.Gold:
            Player.Instance.gold -= price;
            break;
         case EWealth.Jewel:
            Player.Instance.jewel -= price;
            break;
      }
      
      var swp = Instantiate(summonWindowPrefab, transform.parent.parent);
      swp.GetComponent<CSummonWindow>().rank = receivedStreamer.Rank;
      swp.GetComponent<CSummonWindow>().streamer = receivedStreamer;
      Debug.Log($"랭크 {receivedStreamer.Rank}, {receivedStreamer.Tag} 당첨");
   }

   public static void Save()
   {
      uint[] numRecruit = new uint[3] {_numNewRecruit,_numExpertRecruit,_numFreeRecruit};
      CSaveLoadManager.CreateJsonFileForArray(numRecruit,"SaveFiles","Recruit");
   }

   void Load()
   {
      uint[] numRecruit =CSaveLoadManager.LoadJsonFileToArray<uint>("SaveFiles", "Recruit");
      if (numRecruit == null)
         return;
      _numNewRecruit = numRecruit[0];
      _numExpertRecruit = numRecruit[1];
      _numFreeRecruit = numRecruit[2];

      if (CTimeManager.LastRealTime.Date != DateTime.Today)
      {
         _numNewRecruit = 0;
         _numExpertRecruit = 0;
         _numFreeRecruit = 0;
      }
   }
   
}



