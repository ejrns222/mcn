using System.Collections.Generic;
using Characters;
using UnityEngine;

public class Player : MonoBehaviour
{
   public static Player Instance = null;
   public StreamerBase[] equippedStreamers = new StreamerBase[8];
   
   /////////////////////플레이어 스텟///////////////////////////
   [SerializeField] public uint maxNumStreamers =0;
   [SerializeField] private long basicSalary = 0;
   [SerializeField] private long editPay = 0;
   public long BasicSalary => basicSalary;
   public long EditPay => editPay;
   public float ProbBoss => CSelfCare.CareSkill.SocialLevel * 0.2f /100f;
   public float probLord => CDimensionResearch.ResearchSkill.DBroadLevel / 500f;
   
   ////////////////////////재화/////////////////////////////////
   public long mileage;
   public long gold;
   public long jewel;
   
   private void Awake()
   {
      if (!Instance)
         Instance = this;
      else if(Instance != this)
         Destroy(gameObject);
      DontDestroyOnLoad(gameObject);

      Load();
   }

   public bool FindStreamer(EStreamer streamerName)
   {
      for (int i = 0; i < equippedStreamers.Length; i++)
      {
         if(equippedStreamers[i] == null)
            continue;
         if (equippedStreamers[i].Tag == streamerName)
            return true;
      }
      return false;
   }

   public void Save()
   {
      long[] wealths = new long[3]{mileage,gold,jewel};
      CSaveLoadManager.CreateJsonFileForArray(wealths,"SaveFiles","Wealths");
      
      if (equippedStreamers.Length == 0)
      {
         return;
      }
      List<string> saveStreamer = new List<string>();
      foreach (var v in equippedStreamers)
      {
         saveStreamer.Add(StreamerBaseForJson.StreamerToString(v));
      }
      CSaveLoadManager.CreateJsonFileForArray(saveStreamer.ToArray(),"SaveFiles","EquippedStreamer");
   }

   private void Load()
   {
      long[] wealths = CSaveLoadManager.LoadJsonFileToArray<long>("SaveFiles", "Wealths");
      if (wealths != null)
      {
         mileage = wealths[0];
         gold = wealths[1];
         jewel = wealths[2];
      }

      ////////TEST////////
      mileage = 10000000000;
      gold = 200000000000000;
      jewel = 1000;
      /////////////////////
      
      var streamers = CSaveLoadManager.LoadJsonFileToArray<string>("SaveFiles","EquippedStreamer");
      if(streamers == null)
         return;
      for (int i = 0; i < streamers.Length; i++)
         equippedStreamers[i] = StreamerBaseForJson.StringToStreamer(streamers[i]);

   }
}

