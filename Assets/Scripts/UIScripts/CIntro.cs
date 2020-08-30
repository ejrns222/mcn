using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CIntro : MonoBehaviour
{
    private Text _startText;
    private void Awake()
    {
        _startText = transform.Find("TouchToPlay").GetComponent<Text>();
        Screen.SetResolution(720, 1280,true);
    }

    private void Start()
    {
        StartCoroutine(nameof(FadeInOut)); // 어웨이크는 정확한 호출 시기를 알 수가 없기 떄문에 코루틴은 스타트에서 호출합시다.
    }

    IEnumerator FadeInOut()
    {
         
        bool isFadeOut = true;
        while (true)
        {
            var textColor = _startText.color;
            if (isFadeOut)
            {
                textColor.a -= 0.02f;
                if (textColor.a < 0.1f)
                {
                    isFadeOut = false;
                }
            }
            else
            {
                textColor.a += 0.02f;
                if (textColor.a > 0.9f)
                {
                    isFadeOut = true;
                }
            }
            _startText.color = textColor;
            yield return new WaitForSeconds(0.02f);
        }
    }
    
    public void ChangeScene()
    {
        var bools = CSaveLoadManager.LoadJsonFileToArray<bool>("SaveFiles", "TutorialEnd1");
        if (bools == null)
            SceneManager.LoadScene("TutorialScene");
        else 
            SceneManager.LoadScene("GameScene");
    }
    

}
