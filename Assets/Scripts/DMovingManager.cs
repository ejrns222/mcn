using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DMovingManager : MonoBehaviour
{
    [SerializeField] private GameObject tablet = null;
    [SerializeField] private GameObject backGround = null;
    [SerializeField] private GameObject mask = null;

    private void Awake()
    {
        Screen.SetResolution(720,1280,true);
        iTween.MoveTo(tablet, iTween.Hash("y", -190,"isLocal",true, "easeType", "easeOutExpo","delay",2.5f,"Time",1.5f));
        iTween.MoveTo(backGround, iTween.Hash("y", 1.77f, "easeType", "easeOutSine","Time",2.5f,"delay",0.5f));
        StartCoroutine(FadeInOut(true,0.5f));
    }

    public void ExitDimensionDoor()
    {
        iTween.MoveTo(tablet, iTween.Hash("y", -1064,"isLocal",true, "easeType", "easeOutExpo","Time",1.5f));
        iTween.MoveTo(backGround, iTween.Hash("y", -1.77f, "easeType", "easeOutSine","Time",2.5f,"delay",1));
        StartCoroutine(FadeInOut(false,0.5f,"GameScene"));
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
    }
}
