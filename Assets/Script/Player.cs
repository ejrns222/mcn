using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   private UInt64 _mileage;
   public UInt64 Mileage 
   {
      get { return _mileage; }
      set { _mileage = value; } 
   } //관리하는 캐릭터들의 강화, 캐스팅, 굿즈 구입에 사용되는 재화
   public uint Jewel { get; set; } //유료재화
   public UInt64 Money { get; set; } //플레이어의 강화에 사용되는 재화

   
   
   private static Player instance;
   
   ////////////////////////TEST///////////////////////////// 배열에 넣어서 스트리머와 유튜버를 관리하는 방식으로 만들자
   private DDeokuni _deokuni;
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

      Mileage = 0;
      Jewel = 0;
      Money = 0;
      ////////////////////////TEST/////////////////////////////
      _deokuni = new DDeokuni();
      /// //////////////////////////////////////////////////
   }

   private void FixedUpdate()
   {
      Mileage++;
      Jewel += 2;
      Money += 3;
      
      ////////////////////////TEST///////////////////////////// 프로퍼티로 사용할수 있게 바꿔보자. 혹은 1안. 마일리지 같은 재화를 클래스로 만들어서 재화가 증가하는 것을 계산해주는 함수를 만든다. 2안. 델리게이트를 만들고 각 캐릭터들이 계산함수를 들고있고 델리게이트에 전부 넣어버린다면?
      _deokuni.Update(ref _mileage);
      ///  /// //////////////////////////////////////////////////
      
   }
}

