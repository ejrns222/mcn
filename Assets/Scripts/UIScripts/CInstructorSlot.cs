using Characters;
using UnityEngine;
using UnityEngine.UI;

public class CInstructorSlot : MonoBehaviour
{
    public InstructorBase Instructor;
    public GameObject InfoWindowPrefab;

    private void Start()
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("CharacterImage/Instructors/" + Instructor.Tag);
        Refresh();

    }

    private void OnEnable()
    {
        Refresh();
    }

    private void Refresh()
    {
        foreach (var v in CDictionary.Instructors)
        {
            if (Instructor == null)
               return;
            
            if (v.Key.Tag == Instructor.Tag)
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
                    transform.Find("Name").GetComponent<Text>().text = Instructor.Name;
                    break;
                }
            }
        }
    }
    
    
    public void OnClick()
    {
        foreach (var v in CDictionary.Instructors)
        {
            if (v.Key.Tag == Instructor.Tag)
            {
                if (v.Value == false)
                {
                    var window = Instantiate(InfoWindowPrefab, GameObject.Find("Display").transform);
                    window.transform.Find("Scroll View/Viewport/Content/Name").GetComponent<Text>().text = "이름 : ???" ;
                    window.transform.Find("Scroll View/Viewport/Content/Desc1").GetComponent<Text>().text = "- ???" ;
                    window.transform.Find("Scroll View/Viewport/Content/Skill").GetComponent<Text>().text = "스킬 : ???";
                    window.transform.Find("Scroll View/Viewport/Content/Desc2").GetComponent<Text>().text = "- ???";
                    window.transform.Find("Scroll View/CloseButton").GetComponent<Button>().onClick.AddListener(delegate { Destroy(window); });
                }
                else
                {
                    var window = Instantiate(InfoWindowPrefab, GameObject.Find("Display").transform);
                    window.transform.Find("Scroll View/Viewport/Content/Name").GetComponent<Text>().text = "이름 : " + Instructor.Name;
                    window.transform.Find("Scroll View/Viewport/Content/Desc1").GetComponent<Text>().text = "-" + Instructor.Desc;
                    window.transform.Find("Scroll View/Viewport/Content/Skill").GetComponent<Text>().text = "스킬 : " + Instructor.SkillName;
                    window.transform.Find("Scroll View/Viewport/Content/Desc2").GetComponent<Text>().text = "-" + Instructor.SkillDesc;
                    window.transform.Find("Scroll View/CloseButton").GetComponent<Button>().onClick.AddListener(delegate { Destroy(window); });
                }
            }
        }

        
    }

}
