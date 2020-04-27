using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Util;
using Wealths;

public class Player : MonoBehaviour
{
   public static Player Instance = null;
   public List<GameObject> streamers = new List<GameObject>();
   public uint basicSalary = 1000;

   private delegate void wealthIncreaseHandler(uint value);

   private wealthIncreaseHandler onIncreaseMileage;

   private void Awake()
   {
      if (!Instance)
         Instance = this;
      else if(Instance != this)
         Destroy(gameObject);
      DontDestroyOnLoad(gameObject);
      
      onIncreaseMileage = new wealthIncreaseHandler(GameObject.Find("Mileage").transform.Find("Text").GetComponent<CWealthRenderer>().RenderEarnedWealth);
      if(onIncreaseMileage == null)
         Debug.Log("delegate null");

      ////////////////////////TEST/////////////////////////////
      //streamers.Add(Streamer.MakeStreamer(EStreamer.TestHun));
      // streamers.Add(Streamer.MakeStreamer(EStreamer.TestHyun));
      //////////////////////////////////////////////////////////
      
      StartCoroutine(nameof(IncreaseMileage));
   }

   public bool FindStreamer(EStreamer streamerName)
   {
      foreach (var v in streamers)
      {
        if(v.GetComponent<IStreamer>().Tag == streamerName)
            return true;
      }

      return false;
   }

   private IEnumerator IncreaseMileage()
   {
      while (true)
      {
         uint calculatedSalary = CalcRealSalary();
         Mileage.Instance.Value += calculatedSalary;
         onIncreaseMileage(calculatedSalary);

         yield return new WaitForSeconds(1f);
      }
   }

   public uint CalcRealSalary()
   {
      uint realSalary = basicSalary;
      foreach (var v in streamers)
      {
         realSalary += v.GetComponent<IStreamer>().Skill();
      }

      return realSalary;
   }
}

