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

    public void SetContentSwitch(int numContent)
    {
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
