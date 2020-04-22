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

   private void Awake()
   {
      if (!Instance)
         Instance = this;
      else if(Instance != this)
         Destroy(gameObject);
      DontDestroyOnLoad(gameObject);

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
        // if (v.Tag == streamerName)
        if(v.GetComponent<IStreamer>().Tag == streamerName)
            return true;
      }

      return false;
   }

   private IEnumerator IncreaseMileage()
   {
      while (true)
      {
         uint factor = 100;
         foreach (var v in streamers)
         {
            // factor += v.Skill();
            factor += v.GetComponent<IStreamer>().Skill();
         }
         Mileage.Instance.Value += factor;
         yield return new WaitForSeconds(1f);
      }
   }
}

