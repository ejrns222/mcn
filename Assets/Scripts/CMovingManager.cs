using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CMovingManager : MonoBehaviour
{
    [SerializeField] private GameObject tablet = null;
    [SerializeField] private GameObject room = null;
    [SerializeField] private GameObject backGround = null;
    [SerializeField] private GameObject mask = null;
    [SerializeField] private GameObject tutorial = null;
    

    private void Awake()
    {
        Screen.SetResolution(720,1280,true);
        iTween.MoveTo(tablet, iTween.Hash("y", -190,"isLocal",true, "easeType", "easeOutExpo","delay",2f,"Time",1.5f));
        iTween.MoveTo(room, iTween.Hash("y", 0, "easeType", "easeOutSine","Time",1f,"delay",0.5f));
        iTween.MoveTo(backGround, iTween.Hash("y", 7, "easeType", "easeOutSine","Time",1f,"delay",0.5f));
        StartCoroutine(FadeInOut(true,0.5f));

        
    }

    public void ExitOffice()
    {
        iTween.MoveTo(tablet, iTween.Hash("y", -1064,"isLocal",true, "easeType", "easeOutExpo","Time",1.5f));
        iTween.MoveTo(room, iTween.Hash("y", -245,"isLocal",true, "easeType", "easeOutSine","Time",1f,"delay",1));
        iTween.MoveTo(backGround, iTween.Hash("y", 5, "easeType", "easeOutSine","Time",1f,"delay",1));
        StartCoroutine(FadeInOut(false,0.5f,"DimensionDoorScene"));
        CSaveLoadManager.ClassesSave();
    }

    /// <summary>
    /// 페이드 효과를 주고 끝나면서 씬전환
    /// </summary>
    /// <param name="tnf">true면 In false면 out</param>
    /// <param name="delay">딜레이 시간</param>
    /// <param name="sceneName">전환할 씬 이름</param>
    /// <returns></returns>
    IEnumerator FadeInOut(bool tnf, float delay = 0,string sceneName ="" )
    {
        Image img = mask.GetComponent<Image>();
        if(tnf)
            img.color = Color.black;
        yield return new WaitForSeconds(delay);
        while (true)
        {
            var imgColor = img.color;
            
            if(tnf)
                imgColor.a -= 0.02f;
            else
                imgColor.a += 0.02f;
            
            img.color = imgColor;
            yield return new WaitForSeconds(0.05f);

            if (imgColor.a >= 1 || imgColor.a <= 0)
            {
                Debug.Log("페이드코루틴 끝");
                if(!tnf)
                    SceneManager.LoadScene(sceneName);
                break;
            }
        }
        var isTutorialEnd =CSaveLoadManager.LoadJsonFileToArray<bool>("SaveFiles", "TutorialEnd2");
        if(isTutorialEnd == null)
            tutorial.SetActive(true);
    }
}
