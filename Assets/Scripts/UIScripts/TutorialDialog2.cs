using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDialog2 : MonoBehaviour
{
    private bool[] _isTutorialEnd;
    public GameObject touchMask;
    void Awake()
    {
        _isTutorialEnd =CSaveLoadManager.LoadJsonFileToArray<bool>("SaveFiles", "TutorialEnd2");
        if(_isTutorialEnd != null)
        {
            Destroy(touchMask);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Start()
    {
        var dm = CDialogManager.CreateDialog
        (
            "Sajang:여기가 앞으로 일하실 사무실입니다. 태블릿은 받으셨죠?", 
            "Me:네. 좀 싸구려 같긴한데 쓸만하네요. 이제 뭘 하면 되나요?",
            "Sajang:일단 저희 회사가 어떤 일을 하는지는 이해하셨나요?",
            "Me:전도유망한 초보 스트리머들을 채용과 동시에 교육한 뒤 팔아먹는다.. 맞나요.",
            "Sajang:\"이직을 도와준다\". 라는 표현이 더 좋을 것 같군요. 아무튼 잘 이해하셨습니다. 그렇다면 우선 채용을 한번 해볼까요?"
        );
        dm.AddOnDialogEnd(Dm1End);
    }

    void Dm1End()
    {
        var hl = CUIHighlight.CreateHighlight(GameObject.Find("ButtonsPanel/Employment"));
        hl.AddOnHighlightEnd(Dm2);
    }

    void Dm2()
    {
        var dm = CDialogManager.CreateDialog
        (
            "Sajang:참고로 매니저님이 가진 모든 재화는 태블릿 상단에 실시간으로 표시됩니다. 왼쪽부터 마일리지, 현금, 차원석이죠. 재화들을 버는 방법도 나중에 차근차근 설명해드리겠습니다.",
            "Sajang:일단은 마일리지를 사용해서 채용을 해볼까요?"
        );
        dm.AddOnDialogEnd(Dm2End);
    }

    void Dm2End()
    {
        var hl = CUIHighlight.CreateHighlight(GameObject.Find("GeneralSummon/Button"));
        hl.AddOnHighlightEnd(Dm3);
    }

    void Dm3()
    {
        StartCoroutine(Dm3Coroutine());
    }

    IEnumerator Dm3Coroutine()
    {
        var window = GameObject.Find("SummonWindow(Clone)");
        yield return new WaitForSeconds(8.0f);
        Destroy(window);
        while (true)
        {
            if (window == null)
            {
                var dm = CDialogManager.CreateDialog
                (
                    "Sajang:멋진분을 채용하셨군요. 혹시라도 나중에 똑같은 얼굴, 똑같은 이름을 가진분이 나타나더라도 놀랄 필요는 없습니다. 무수히 많은 평행세계에서 지원을 하기때문이죠.",
                    "Sajang:그리고 아무리 채용을 많이 하더라도 배치를 하지않으면 아무 의미가 없습니다. 배치를 하러 가볼까요?"
                );
                dm.AddOnDialogEnd(Dm3End);
                
                yield break;
            }

            yield return null;
        }
    }

    void Dm3End()
    {
        var hl = CUIHighlight.CreateHighlight(GameObject.Find("MonitoringButton"));
        hl.AddOnHighlightEnd(Dm4);
    }

    void Dm4()
    {
        var hl = CUIHighlight.CreateHighlight(GameObject.Find("EquipSlot1/Button"));
        hl.AddOnHighlightEnd(Dm4_1);
    }

    void Dm4_1()
    {
        var hl = CUIHighlight.CreateHighlight(GameObject.Find("CharacterChangeSlot(Clone)"));
        hl.AddOnHighlightEnd(Dm4_2);
    }

    void Dm4_2()
    {
        var hl = CUIHighlight.CreateHighlight(GameObject.Find("CharacterChangeWindow/Panel/SelectButton"));
        hl.AddOnHighlightEnd(Dm5);
    }

    void Dm5()
    {
        var dm = CDialogManager.CreateDialog
        (
            "Sajang:이렇게 배치가 되면 이제부터 스트리머가 벌어들인 일정 수익이 매니저님의 마일리지로 적립이 된답니다.",
            "Sajang:그리고 저희 회사의 자랑 인공지능 영상 편집기 \"드리미어\"가 자동으로 너튜브에 올라갈 영상을 편집하기 시작하죠."
        );
        dm.AddOnDialogEnd(Dm5End);
    }

    void Dm5End()
    {
        var hl = CUIHighlight.CreateHighlight(GameObject.Find("VideoEditButton"));
        hl.AddOnHighlightEnd(Dm6);
    }

    void Dm6()
    {
        var dm = CDialogManager.CreateDialog
        (
            "Sajang:영상은 기본적으로 저희 차원시간으로 매일 일인당 하나가 만들어지고, 만들어진 영상만큼 매니저님이 일당을 받게됩니다. 가만히 있어도 돈이 들어오는군요. 엄청나지 않습니까?",
            "Sajang:스트리머들도 주로 영상으로 구독자가 유입됩니다. 구독자가 한계치에 다다르면 이직을 할 수 있는 조건이 충족됩니다.",
            "Sajang:그리고 혹시 구독자를 빠르게 늘리고 싶으시다면 사비를 들여서 광고를 하는 방법도 있습니다. 광고를 할 때 마다 점점 비싸진다는걸 알아두시길 바랍니다.",
            "Sajang:자기관리와 차원연구는 각각 현금과 차원석을 써서 능력을 강화시키는 곳인데 어렵지는 않으니까 별도로 설명은 안드리겠습니다.",
            "Sajang:마지막으로 차원관문으로 가볼까요?"
        );
        dm.AddOnDialogEnd(Dm6_1);
    }

    void Dm6_1()
    {
        var hl = CUIHighlight.CreateHighlight(GameObject.Find("Gateway"));
        hl.AddOnHighlightEnd(Dm6End);
    }

    void Dm6End()
    {
        var hl = CUIHighlight.CreateHighlight(GameObject.Find("DimensionSlot/Button"));
        
        bool[] bools = new bool[1]{true};
        CSaveLoadManager.CreateJsonFileForArray(bools,"SaveFiles","TutorialEnd2");
    }
}