using Characters;
using UnityEngine;
using UnityEngine.UI;

public class CDictionarySlot : MonoBehaviour
{
    public StreamerBase streamer;

    private void Start()
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("CharacterImage/" + streamer.Tag);
        Refresh();

    }

    private void OnEnable()
    {
        Refresh();
    }

    private void Refresh()
    {
        foreach (var v in CDictionary.AllStreamers)
        {
            if (streamer == null)
               return;
            
            if (v.Key.Tag == streamer.Tag)
            {
                if (v.Value == false)
                {
                    transform.Find("Name").GetComponent<Text>().text = "???";
                    var color = new Color(0.07f,0.07f,0.07f,1);
                    GetComponent<Image>().color = color;
                    break;
                }
                else
                {
                    transform.Find("Name").GetComponent<Text>().text = streamer.Name;
                    break;
                }
            }
        }
    }
    
    //TODO:정보창을 띄운다
    private void OnClick()
    {
    }

}
