using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPopUpWindow : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = GetComponentInChildren<Button>();
        _button.onClick.AddListener(FClick);
    }

    private void FClick()
    {
        Debug.Log("Click");
        Destroy(gameObject);
    }
}
