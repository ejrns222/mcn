using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class cContentsManager : MonoBehaviour
{
    private Transform[] _childTransform;
    void Awake()
    {
        _childTransform = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            _childTransform[i] = transform.GetChild(i);
        
    }

    /// <summary>
    /// 버튼의 OnClick() 에 장착함
    /// 선택한 컨텐트외 나머지 모두 비활성화
    /// </summary>
    /// <param name="numContent"></param> 활성화 시킬 컨텐트 번호 
    public void SetContentSwitch(int numContent)
    {
        //컨텐트의 번호는 1부터 시작한다.
        if (numContent < 1)
        {
            Debug.Log("1이상만 입렵가능");
            return;
        }

        foreach (var p in _childTransform)
        {
            p.gameObject.SetActive(false);
        }
        GameObject temp = _childTransform[numContent - 1].gameObject;
        
        if(!temp.activeSelf)
            temp.SetActive(true);
    }

}
