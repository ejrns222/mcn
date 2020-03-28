using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public UInt64 Mileage { get; set; } //관리하는 캐릭터들의 강화, 캐스팅, 굿즈 구입에 사용되는 재화
   public uint Jewel { get; set; } //유료재화
   public UInt64 Money { get; set; } //플레이어의 강화에 사용되는 재화
   
   private static Player instance;

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
   }

   private void FixedUpdate()
   {
      Mileage++;
      Jewel += 2;
      Money += 3;
   }
}

