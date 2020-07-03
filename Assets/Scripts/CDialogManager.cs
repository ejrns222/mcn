using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CDialogManager : MonoBehaviour
{
    [SerializeField]
    public GameObject dialog;
    public GameObject myDialog;
    private List<GameObject> _dialogObjects;
    public string[] dialogs;
    private string _prevName;
    private int _currentDialogIdx = 0;

    private void Awake()
    {
        _prevName = string.Empty;
        _dialogObjects = new List<GameObject>();
        foreach (var v in dialogs)
        {
            var nameAndText = v.Split(':');
            if (nameAndText.Length == 0 || nameAndText == null)
                break;
            
            if (nameAndText[0] == "Me" && nameAndText[0] != _prevName)
            {
                var gobj = Instantiate(myDialog,GameObject.Find("Canvas").transform);
                gobj.GetComponent<CDialog>().SetName("나");
                gobj.GetComponent<CDialog>().AddText(nameAndText[1]);
                _dialogObjects.Add(gobj);
                _prevName = nameAndText[0];
            }
            else if(nameAndText[0] != "Me" && nameAndText[0] != _prevName)
            {
                var gobj = Instantiate(dialog,GameObject.Find("Canvas").transform);
                gobj.GetComponent<CDialog>().SetName(nameAndText[0]);
                gobj.GetComponent<CDialog>().SetCharacter(nameAndText[0]);
                gobj.GetComponent<CDialog>().AddText(nameAndText[1]);
                _dialogObjects.Add(gobj);
                _prevName = nameAndText[0];
            }
            else if (nameAndText[0] == _prevName)
            {
                _dialogObjects[_dialogObjects.Count-1].GetComponent<CDialog>().AddText(nameAndText[1]);
            }
        }

        foreach (var v in _dialogObjects)
        {
            v.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (_dialogObjects[_currentDialogIdx] == null)
        {
            _currentDialogIdx++;
            if (_currentDialogIdx >= _dialogObjects.Count)
            {
                Destroy(gameObject);
                SceneManager.LoadScene("GameScene");//임시
                return;
            }
        }
        
        if (_dialogObjects[_currentDialogIdx].activeSelf == false )
        {
            _dialogObjects[_currentDialogIdx].SetActive(true);
        }
        
        
    }
}
