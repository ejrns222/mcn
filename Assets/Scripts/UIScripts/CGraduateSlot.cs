using System.Security.Cryptography.X509Certificates;
using Characters;
using UnityEngine;
using UnityEngine.UI;
using Wealths;

public class CGraduateSlot : MonoBehaviour
{
    public StreamerBase streamer;
    public long resultPrice;
    public bool isSelected = false;

    private void Start()
    {
        transform.Find("Name").GetComponent<Text>().text = streamer.Tag.ToString();
        transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("CharacterImage/" + streamer.Tag);
        transform.Find("Desc").GetComponent<Text>().text = "랭크 : " +streamer.Rank;
        transform.Find("AdLevel").GetComponent<Text>().text = "광고횟수 : " + streamer.AdLevel;

        resultPrice = (long)(100 *  Mathf.Pow(1.5f, 5 - (float) streamer.Rank));
        resultPrice = (long) (resultPrice * (1f + ((float) streamer.AdLevel / 10)));
        transform.Find("Button/PriceText").GetComponent<Text>().text =
            UnitConversion.ConverseUnit(resultPrice).ConversedUnitToString();
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    public void OnClick()
    {
        isSelected = !isSelected;
        if (isSelected)
        {
            transform.Find("Button/Text").GetComponent<Text>().text = "해제";
            GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f,1);
            transform.Find("Image").GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f,1);
        }
        else
        {
            transform.Find("Button/Text").GetComponent<Text>().text = "선택";
            GetComponent<Image>().color = Color.white;
            transform.Find("Image").GetComponent<Image>().color = Color.white;
        }
    }

    public void Graduation()
    {
        int idx = CInventory.FindStreamerIndex(streamer);
        if (idx < 0)
        {
            Debug.Log("스트리머 졸업 오류");
            return;
        }
        Player.Instance.jewel += resultPrice;
        CInventory.streamerList[idx] = null;
        CWealthRenderer.RenderEarnedWealth(resultPrice,EWealth.Jewel);
        Destroy(gameObject);
    }
}
