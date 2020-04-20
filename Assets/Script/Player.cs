using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   
   public uint _mileage; //클래스화 시키자
   //public UInt64 Mileage 
   //{
   //   get { return _mileage; }
   //   set { _mileage = value; } 
   //} //관리하는 캐릭터들의 강화, 캐스팅, 굿즈 구입에 사용되는 재화
   public uint Jewel { get; set; } //유료재화
   public UInt64 Money { get; set; } //플레이어의 강화에 사용되는 재화

   delegate void MileageCalcDelegate();

   
   
   private static Player instance;
   
   ////////////////////////TEST/////////////////////////////
   
   public List<Streamer> _streamers = new List<Streamer>();
   public List<Neotuber> _neotubers = new List<Neotuber>();
   /// //////////////////////////////////////////////////////
  
   
   
   public static Player Instance
   {
      get
      {
         if (instance == null)
         {
            var obj = FindObjectOfType<Player>();
            if (obj != null)
            {
               instance = obj;
            }
            else
            {
               var newSinglton = new GameObject("Player").AddComponent<Player>();

               instance = newSinglton;
            }
         }

         return instance;
      }
      private set
      {
         instance = value;
      }
   }

   private void Awake()
   {
      var objs = FindObjectsOfType<Player>();
      if (objs.Length != 1)
      {
         Destroy(gameObject);
         return;
      }
      DontDestroyOnLoad(gameObject);

     // Mileage = 0;
      Jewel = 0;
      Money = 0;
      ////////////////////////TEST/////////////////////////////
      _streamers.Add(Streamer.MakeStreamer(EStreamer.TestHun));
      _streamers.Add(Streamer.MakeStreamer(EStreamer.TestHyun));
      foreach (var v in _streamers)
      {
         v.TestLog();
      }

      StartCoroutine("IncreaseMileage");
   }

   private void FixedUpdate()
   {


      
      ////////////////////////TEST///////////////////////////// 프로퍼티로 사용할수 있게 바꿔보자. 혹은 1안. 마일리지 같은 재화를 클래스로 만들어서 재화가 증가하는 것을 계산해주는 함수를 만든다. 2안. 델리게이트를 만들고 각 캐릭터들이 계산함수를 들고있고 델리게이트에 전부 넣어버린다면?

   }

   public bool FindStreamer(EStreamer streamerName)
   {
      foreach (var v in _streamers)
      {
         if (v.Tag == streamerName)
            return true;
      }

      return false;
   }

   IEnumerator IncreaseMileage()
   {
      
      while (true)
      {
         uint factor = 1;
         foreach (var v in _streamers)
         {
            factor += v.Skill();
         }
         _mileage += factor;
         yield return new WaitForSeconds(1f);
      }
   }
}

