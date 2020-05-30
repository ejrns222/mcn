using System;
using System.Collections;
using System.Collections.Generic;
using Characters;
using Characters.Streamers;
using UnityEngine;
using Util;

public class Player : MonoBehaviour
{
   public static Player Instance = null;
   public List<IStreamer> equippedStreamers = new List<IStreamer>();
   //TODO : 캐릭터들 MonoBehavior상속 모두 뺴버리고 IStreamer로 사용하게 끔 한뒤 생성자나 업데이트문으로 필요한거 있으면 돌리는게 좋을 듯
   
   /////////////////////플레이어 스텟///////////////////////////
   public uint maxNumStreamers;
   public float probA = 0;
   public float probB = 0;
   public float probC = 0.03f;
   public float probD = 0.15f;
   //public float probF = 0.82f; 나머지
   public uint basicSalary = 50000;
   public uint editPay = 100;
   public float probBoss = 0f;
   public float probLord = 0f;
   ////////////////////////////////////////////////////////////
  

   private void Awake()
   {
      if (!Instance)
         Instance = this;
      else if(Instance != this)
         Destroy(gameObject);
      DontDestroyOnLoad(gameObject);

      equippedStreamers.Add(new CTestHun());
      equippedStreamers.Add(new CTestHyun());
      
   }

   public bool FindStreamer(EStreamer streamerName)
   {
      foreach (var v in equippedStreamers)
      {
        if(v.Tag == streamerName)
            return true;
      }

      return false;
   }
}

