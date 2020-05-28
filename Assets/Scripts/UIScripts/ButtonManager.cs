using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private Button[] _childButton;
    void Awake()
    {
        _childButton = gameObject.GetComponentsInChildren<Button>();
    }

    /// <summary>
    /// 버튼을 누르면 해당 버튼만 색상 변경
    /// TODO: 현재는 아무것도 안하는데 나중에 이미지를 구하면 이미지를 바꾸는 방식으로 변경해야함 (interactable->sprite)
    /// </summary>
    /// <param name="num">버튼의 번호 버튼은 1번부터임</param>
    public void SetButtonColor(int num)
    {
        foreach (var p in _childButton)
        {
            var colorBlock = p.colors;
            colorBlock.normalColor = Color.white;
            p.colors = colorBlock;
        }
        GameObject temp = _childButton[num - 1].gameObject;

        var selectedColorBlock = _childButton[num - 1].colors;
        selectedColorBlock.normalColor = Color.green;
        _childButton[num - 1].colors = selectedColorBlock;
    }
    
}
