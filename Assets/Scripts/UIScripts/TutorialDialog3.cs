using System;
using UnityEngine;

public class TutorialDialog3 : MonoBehaviour
{
    private bool[] _isTutorialEnd;

    void Awake()
    {
        _isTutorialEnd = CSaveLoadManager.LoadJsonFileToArray<bool>("SaveFiles", "TutorialEnd3");
        if (_isTutorialEnd != null)
            Destroy(gameObject);
    }

    private void Start()
    {
        var dm = CDialogManager.CreateDialog
        (
            "Sajang:여기는 제 1 차원관문입니다. 저희가 완벽히 통제 가능한 유일한 관문이고, 이직과 강사소환에 사용되죠.",
            "Sajang:이직을 잘 시켜주면 보상으로 스트리머의 자질과 광고횟수에 따라 차원석을 얻게됩니다.",
            "Sajang:현재는 이직 가능한 스트리머도 없고 차원석도 모자라서 딱히 할게 없으니까 돌아가도록 할까요?",
            "Me:? 아니, 보통 이런건 한번쯤 소환하게 해줘야지!",
            "Sajang:차원석은 굉장히 귀한거라서.. 아무튼 기본 교육은 여기까지고 사무실에 설명책자가 있으니 궁금한건 연락하지 마시고 책자를 참고하시면 됩니다.",
            "Me:뭔가 좀 무책임한데... 일단 알겠습니다.",
            "Sajang:그럼 그렇게 알고 돌아가시는 길은 아시겠죠? 저는 바로 출장을 가야해서 여기서 헤어지도록 하죠. 아마 곧 만날지도 모릅니다...후후..",
            "Me:이상하게 웃지말고 빨리 가버려요.",
            "Sajang:네 그럼 이만.",
            "Me:갑작스럽게 엄청난 일을 겪어서 정신이 하나도 없네.. 일단 사무실로 돌아가자."
        );
        dm.AddOnDialogEnd(Dm1_1);
    }

    void Dm1_1()
    {
        var hl = CUIHighlight.CreateHighlight(GameObject.Find("ButtonsPanel/BackButton"));
        hl.AddOnHighlightEnd(Dm1End);
    }

    void Dm1End()
    {
        var hl = CUIHighlight.CreateHighlight(GameObject.Find("BackToOffice/Panel/Button"));
        //hl.AddOnHighlightEnd(Dm1End);
        bool[] bools = new bool[1]{true};
        CSaveLoadManager.CreateJsonFileForArray(bools,"SaveFiles","TutorialEnd3");
    }

}
