using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cButtonManager : MonoBehaviour
{
    private Transform[] _childTransform;
    void Awake()
    {
        _childTransform = gameObject.GetComponentsInChildren<Transform>();
    }

    
}
