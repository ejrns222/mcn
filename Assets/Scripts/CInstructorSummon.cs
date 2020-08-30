using System;
using System.Collections;
using System.Linq;
using Characters;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CInstructorSummon : MonoBehaviour
{
    public GameObject backGround;
    public GameObject summonEffect;
    public GameObject choiceInstructor;
    public GameObject instructorInfoWindow;

    public void Summon()
    {
        bool isAllGot = true;
        foreach (var v in CDictionary.Instructors.Values)
        {
            if (!v)
                isAllGot = false;
        }

        if (isAllGot)
        {
            var pw = Instantiate(Resources.Load("UIPrefabs/PopUpWindow") as GameObject,transform.root);
            pw.transform.Find("Panel").Find("Content").GetComponent<Text>().text = "Error:\n소환할 강사가 없음";
            return;
        }
        
        if (Player.Instance.jewel < 1500)
        {
            var pw = Instantiate(Resources.Load("UIPrefabs/PopUpWindow") as GameObject,transform.root);
            pw.transform.Find("Panel").Find("Content").GetComponent<Text>().text = "Error:\n차원석 부족";
            return;
        }

        Player.Instance.jewel -= 1500;
        GameObject.Find("MovingManager").GetComponent<DMovingManager>().TabletDown();
        StartCoroutine(ImgShakingVertical(backGround.transform, -0.002f, 0.05f, 3));
        StartCoroutine(EffectOn());

        int num;
        while (true)
        {
            num = Random.Range(0, CDictionary.Instructors.Count);
            if (!CDictionary.Instructors.ElementAt(num).Value)
                break;
        }

        var result = (InstructorBase)Activator.CreateInstance(CDictionary.Instructors.ElementAt(num).Key.GetType());
        choiceInstructor.GetComponent<Image>().sprite =
            Resources.Load<Sprite>("CharacterImage/Instructors/" + result.Tag);
        instructorInfoWindow.transform.Find("Panel/Name").GetComponent<Text>().text = "이름 : " + result.Name;
        instructorInfoWindow.transform.Find("Panel/Desc1").GetComponent<Text>().text = "-" + result.Desc;
        instructorInfoWindow.transform.Find("Panel/Skill").GetComponent<Text>().text = "스킬 : " + result.SkillName;
        instructorInfoWindow.transform.Find("Panel/Desc2").GetComponent<Text>().text = "-" + result.SkillDesc;
        
        CInventory.instructorList.Add(result);
        foreach (var v in CDictionary.Instructors)
        {
            if (v.Key.Tag == result.Tag)
            {
                CDictionary.Instructors[v.Key] = true;
                break;
            }
        }
    }

    public void OnExitButton()
    {
        instructorInfoWindow.SetActive(false);
        choiceInstructor.transform.localPosition = new Vector3(0,-49,0);
        choiceInstructor.SetActive(false);
        GameObject.Find("MovingManager").GetComponent<DMovingManager>().TabletUp();
    }
    
    private IEnumerator ImgShakingVertical(Transform transform, float decay, float intensity, float time)
    {
        Vector3 originPosition = backGround.transform.position;
        float animSpeed = 1;

        while (time >= 0)
        {
            float positionY = originPosition.y + Random.insideUnitSphere.y * intensity;
            Vector3 tempPosition = originPosition;
            tempPosition.y = positionY;
            transform.position = tempPosition;

            yield return new WaitForSeconds(0.02f);
            time -= 0.02f;
            intensity -= decay;
            if (intensity < 0)
                intensity = 0;
            
            backGround.GetComponent<Animator>().SetFloat("Speed",animSpeed+=0.03f);
        }
        
        backGround.GetComponent<Animator>().SetFloat("Speed",1);
        transform.position = originPosition;
        
    }

    private IEnumerator EffectOn()
    {
        yield return new WaitForSeconds(2f);
        var effectImg = summonEffect.GetComponent<Image>();
        Color tempImg = Color.white;
        tempImg.a = 0.5f;
        float elapsedTime = 0;
        
        Vector3 scale = new Vector3(0,1,1);
        instructorInfoWindow.transform.localScale = scale;
        instructorInfoWindow.SetActive(true);
        while (true)
        {
            if (elapsedTime > 2)
            {
                if (!choiceInstructor.activeSelf)
                {
                    choiceInstructor.SetActive(true);
                    iTween.MoveTo(choiceInstructor,iTween.Hash("y",-150,"isLocal",true,"easeType","easeOutCubic","Time",2.5f,"delay",1f));
                }

                tempImg.a -= 0.01f;
                effectImg.color = tempImg;
            }
            else
            {
                tempImg.a += 0.01f;
                if (tempImg.a > 1)
                    tempImg.a = 1;
                effectImg.color = tempImg;
                summonEffect.transform.localScale *= 1.1f;
                elapsedTime += 0.02f;
            }
            
            if (tempImg.a<=0)
            {
                summonEffect.transform.localScale = Vector3.one;
                scale.x += 0.2f;
                instructorInfoWindow.transform.localScale = scale;
                if(scale.x >= 1)
                    yield break;
            }
            
            yield return new WaitForSeconds(0.02f);
        }
    }
}
