using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialDialog : MonoBehaviour
{
    public Image BG;
    public Image Paper;
    
    private void Awake()
    {
        var dm = CDialogManager.CreateDialog
            (
                "Me:으으..여기가 어디지..?",
                "Me:분명 방금 전까지 회사에 있었는데.."
            );
        dm.AddOnDialogEnd(Dm1End);
    }

    private void Dm1End()
    {
        StartCoroutine(Dm2());
    }
    
    IEnumerator Dm2( )
    {
        Image img = BG;
        
        while (true)
        {
            var imgColor = img.color;
            imgColor.a += 0.02f;
            img.color = imgColor;
            yield return new WaitForSeconds(0.05f);

            if (imgColor.a >= 1 )
            {
                var dm = CDialogManager.CreateDialog
                (
                    "Me:으앗!!! 이건 뭐야??",
                    "Sajang:아. 정신이 드셨군요.",
                    "Me:우왁! 누..누구세요?!",
                    "Sajang:소개가 늦었군요. 저는 다차원 개인방송인 육성 아카데미 대표, BJ대표입니다.",
                    "Me:다차원..뭐요? 그것보다 이름이 왜그래요?",
                    "Sajang:어려우시면 그냥 DSA로 알고계시면 됩니다. 그리고 저희 학원은 소속된 모든 사람들은 BJ명으로 활동한답니다. 물론 교직원도 포함입니다. 실제로 방송도 하고있지요.",
                    "Me:(구려...)저.. 근데 제가 여기 어떻게 오게된거죠? 분명 회사였는데 바닥에서 빛이 확 나더니 갑자기 여기로 왔어요.",
                    "Sajang:놀라셨겠군요. 사실 당신은 저희 학원의 특별 매니저로 초빙된겁니다. 제가 당신을 소환했죠.",
                    "Me:?? 소환이요? 내가 지금 꿈을 꾸고있나..",
                    "Sajang:이곳은 당신이 있던 차원과 다른 차원입니다. 특히 차원이동에 특화되어있죠. 그쪽 차원에서 올해의 매니저상을 수상하셨더군요. 맞나요?",
                    "Me:네..맞긴 맞는데요.. 결론부터 말하자면 전 집에 돌아가고 싶은데요..",
                    "Sajang:그럼 저도 결론부터 말씀드려야겠군요. 여기 계약서입니다. 한번 보기라도 하고 결정하시죠?"
                );
                dm.AddOnDialogEnd(Dm2End);
                yield break;
            }
        }
    }

    void Dm2End()
    {
        StartCoroutine(Dm3());
    }

    IEnumerator Dm3()
    {
        Image img = Paper;
        iTween.MoveTo(img.gameObject, iTween.Hash("y", 0, "easeType", "easeOutExpo","Time",1.5f));
        yield return new WaitForSeconds(3.5f);
        iTween.MoveTo(img.gameObject, iTween.Hash("y", -1200, "easeType", "easeOutExpo","Time",1.5f));
        yield return new WaitForSeconds(1.5f);

        var dm = CDialogManager.CreateDialog
            (
                "Me:가시죠.",
                "Sajang:네?",
                "Me:사무실이 어디냐고요.",
                "Sajang:좋은 선택이십니다. 따라오시죠"
            );
        dm.AddOnDialogEnd(Dm3End);
    }

    void Dm3End()
    {
        StartCoroutine(TutorialEnd());
    }
    IEnumerator TutorialEnd()
    {
        Image img = BG;
        while (true)
        {
            var imgColor = img.color;
            imgColor.a -= 0.02f;
            img.color = imgColor;
            Paper.color = imgColor;
            yield return new WaitForSeconds(0.05f);

            if (imgColor.a < 0)
            {
                SceneManager.LoadScene("GameScene");
                bool[] isTutorialEnd = new bool[1] {true};
                CSaveLoadManager.CreateJsonFileForArray(isTutorialEnd,"SaveFiles","TutorialEnd1");
                yield break;
            }
        }
    }
}
