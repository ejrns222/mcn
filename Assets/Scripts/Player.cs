using System;
using System.Reflection;
using System.Collections.Generic;
using Characters;
using UnityEngine;

public class Player : MonoBehaviour
{
   public static Player Instance = null;
   public StreamerBase[] equippedStreamers = new StreamerBase[8];
   public CInventory inventory;
   
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
      Screen.SetResolution(720,1280,true);
      if (!Instance)
         Instance = this;
      else if(Instance != this)
         Destroy(gameObject);
      DontDestroyOnLoad(gameObject);

      Load();
      
      inventory = new CInventory();
      
      var isTutorialEnd =CSaveLoadManager.LoadJsonFileToArray<bool>("SaveFiles", "TutorialEnd2");
      if (isTutorialEnd == null)
      {
         mileage = 40000;
         jewel = 1500;
      }
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
      
      var streamers = CSaveLoadManager.LoadJsonFileToArray<string>("SaveFiles","EquippedStreamer");
      if(streamers == null)
         return;
      for (int i = 0; i < streamers.Length; i++)
         equippedStreamers[i] = StreamerBaseForJson.StringToStreamer(streamers[i]);

   }
}

public class CInventory
{
   public static List<StreamerBase> streamerList = new List<StreamerBase>();
   public static List<InstructorBase> instructorList = new List<InstructorBase>();

   public CInventory()
   {
      Load();
   }

   public static void Save()
   {
      List<string> saveStreamer = new List<string>();
      List<string> saveInstructor = new List<string>();
      foreach (var v in streamerList)
      {
         saveStreamer.Add(StreamerBaseForJson.StreamerToString(v));
      }

      foreach (var v in instructorList)
      {
         saveInstructor.Add((v.Tag.ToString()));
      }
      CSaveLoadManager.CreateJsonFileForArray(saveStreamer.ToArray(),"SaveFiles","Inventory");
      CSaveLoadManager.CreateJsonFileForArray(saveInstructor.ToArray(),"SaveFiles","InventoryInstructor");
   }

   private void Load()
   {
      var streamers = CSaveLoadManager.LoadJsonFileToArray<string>("SaveFiles","Inventory");
      var instroctors = CSaveLoadManager.LoadJsonFileToArray<string>("SaveFiles","InventoryInstructor");
      if(streamers == null)
         return;
      if (instroctors == null)
         return;
      
      streamerList.Clear();
      instructorList.Clear();
      
      foreach (var v in streamers)
      {
         streamerList.Add(StreamerBaseForJson.StringToStreamer(v));
      }

      foreach (var v in instroctors)
      {
         Type t = Assembly.GetExecutingAssembly().GetType("Characters.Instroctors.C" + v);
         InstructorBase instructor = (InstructorBase) Activator.CreateInstance(t);
         instructorList.Add(instructor);
      }
   }

   public static int FindStreamerIndex(StreamerBase streamer)
   {
      int idx = streamerList.IndexOf(streamer);
      return idx;
   }
}

