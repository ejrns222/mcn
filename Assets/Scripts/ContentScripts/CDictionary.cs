using System;
using System.Collections.Generic;
using System.Reflection;
using Characters;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 모든 스트리머의 정보를 담고 있는 클래스
/// </summary>
public class CDictionary : MonoBehaviour
{
    [SerializeField] private GameObject dictionarySlotPrefab = null;
    
    public static Dictionary<StreamerBase, bool> AllStreamers;
    
    //가차용으로 미리 랭크별 분류해놓은 리스트
    public static List<StreamerBase> StreamersA;
    public static List<StreamerBase> StreamersB;
    public static List<StreamerBase> StreamersC;
    public static List<StreamerBase> StreamersD;
    public static List<StreamerBase> StreamersE;
    public static List<StreamerBase> StreamersF;
    
    private readonly List<GameObject> _rankButtons = new List<GameObject>();
    
    private void Awake()
    {
        //도감 정보
        {
            Init();
        }
        //도감 버튼
        {
            var rankButtons = transform.Find("Content").Find("RankButtons");

            for (int i = 0; i < rankButtons.childCount; i++)
            {
                var v = rankButtons.GetChild(i);
                _rankButtons.Add(v.gameObject);
                v.transform.transform.Find("Scroll View").gameObject.SetActive(false);
            }

            _rankButtons[0].transform.Find("Text").GetComponent<Text>().color = new Color(0, 174 / 255f, 239 / 255f, 1);
            _rankButtons[0].transform.Find("Scroll View").gameObject.SetActive(true);
        }
        
        //도감 내용
        {
            foreach (var v in StreamersA)
            {
                var rankButtonTransform = transform.Find("Content").Find("RankButtons");
                var obj = Instantiate(dictionarySlotPrefab, rankButtonTransform.Find("A").Find("Scroll View").Find("Viewport").Find("Content"));
                obj.GetComponent<CDictionarySlot>().streamer = v;
            }
            
            foreach (var v in StreamersB)
            {
                var rankButtonTransform = transform.Find("Content").Find("RankButtons");
                var obj = Instantiate(dictionarySlotPrefab, rankButtonTransform.Find("B").Find("Scroll View").Find("Viewport").Find("Content"));
                obj.GetComponent<CDictionarySlot>().streamer = v;
            }
            
            foreach (var v in StreamersC)
            {
                var rankButtonTransform = transform.Find("Content").Find("RankButtons");
                var obj = Instantiate(dictionarySlotPrefab, rankButtonTransform.Find("C").Find("Scroll View").Find("Viewport").Find("Content"));
                obj.GetComponent<CDictionarySlot>().streamer = v;
            }
            
            foreach (var v in StreamersD)
            {
                var rankButtonTransform = transform.Find("Content").Find("RankButtons");
                var obj = Instantiate(dictionarySlotPrefab, rankButtonTransform.Find("D").Find("Scroll View").Find("Viewport").Find("Content"));
                obj.GetComponent<CDictionarySlot>().streamer = v;
            }
            
            foreach (var v in StreamersE)
            {
                var rankButtonTransform = transform.Find("Content").Find("RankButtons");
                var obj = Instantiate(dictionarySlotPrefab, rankButtonTransform.Find("E").Find("Scroll View").Find("Viewport").Find("Content"));
                obj.GetComponent<CDictionarySlot>().streamer = v;
            }
            
            foreach (var v in StreamersF)
            {
                var rankButtonTransform = transform.Find("Content").Find("RankButtons");
                var obj = Instantiate(dictionarySlotPrefab, rankButtonTransform.Find("F").Find("Scroll View").Find("Viewport").Find("Content"));
                obj.GetComponent<CDictionarySlot>().streamer = v;
            }
        }
    }

    
    //에셋에 있는 모든 스트리머 클래스들의 정보를 리스트에 넣고, 저장된 도감정보를 가져옴
    private void Init()
    {
        AllStreamers = new Dictionary<StreamerBase, bool>();
        StreamersA = new List<StreamerBase>();
        StreamersB = new List<StreamerBase>();
        StreamersC = new List<StreamerBase>();
        StreamersD = new List<StreamerBase>();
        StreamersE = new List<StreamerBase>();
        StreamersF = new List<StreamerBase>();

        
        foreach (Type t  in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (t.Namespace == "Characters.Streamers")
            {
                StreamerBase temp = (StreamerBase)Activator.CreateInstance(t);
                AllStreamers.Add(temp,false);

                switch (temp.Rank)
                {
                    case ERank.F:
                        StreamersF.Add(temp);
                        break;
                    case ERank.E:
                        StreamersE.Add(temp);
                        break;
                    case ERank.D:
                        StreamersD.Add(temp);
                        break;
                    case ERank.C:
                        StreamersC.Add(temp);
                        break;
                    case ERank.B:
                        StreamersB.Add(temp);
                        break;
                    case ERank.A:
                        StreamersA.Add(temp);
                        break;
                }
            }
        }
        Load();
        
    }

   public void ButtonClick(Button btn)
    {
        foreach (var v in _rankButtons)
        {
            v.transform.Find("Text").GetComponent<Text>().color = Color.black;
            v.transform.transform.Find("Scroll View").gameObject.SetActive(false);
        }

        btn.transform.Find("Text").GetComponent<Text>().color = new Color(0, 174 / 255f, 239 / 255f, 1);
        btn.transform.Find("Scroll View").gameObject.SetActive(true);
    }

    //_allStreamer중 _isDuplicated가 true인 것을 저장
    //true인것만 Dictionary콜렉션에 넣고 저장하는 것이 좋아보인다.
    public static void Save()
    {
        List<string> savedName = new List<string>();
        foreach (var v in AllStreamers)
        {
            if (v.Value == true)
            {
                savedName.Add(v.Key.Tag.ToString());
            }
        }

        CSaveLoadManager.CreateJsonFileForArray(savedName.ToArray(),"SaveFiles","DuplicatedCharacter");
    }
    
    //초기화한 스트리머 리스트의 중복여부를 불러온 리스트에 있는 스트리머의 _isDuplicated로 교체한다.
    private void Load()
    {
        var loadedName = CSaveLoadManager.LoadJsonFileToArray<string>("SaveFiles", "DuplicatedCharacter");
        if (loadedName == null)
            return;
        
        foreach (var v in loadedName)
        {
            foreach (var w in AllStreamers.Keys)
            {
                if (v == w.Tag.ToString())
                {
                    AllStreamers[w] = true;
                    break;
                }
            }
        }
    }
}