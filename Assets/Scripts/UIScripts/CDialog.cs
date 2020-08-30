using System;
using System.Collections;
using System.Collections.Generic;
using Characters;
using UnityEngine;
using UnityEngine.UI;

public class CDialog : MonoBehaviour
{
    private List<string> _texts;
    private Text _text;
    private Image _character;
    private Button _button;
    private bool _isTouch;

    private void Awake()
    {
        _text = transform.Find("Panel").Find("Text").GetComponent<Text>();
        _texts = new List<string>();
        _character = transform.Find("Talker").GetComponent<Image>();
        _button = transform.Find("SkipButton").GetComponent<Button>();
        _isTouch = false;
        _button.onClick.AddListener(Touch);
        iTween.MoveBy(_character.gameObject, iTween.Hash("x", -250,"isLocal",true, "easeType", "easeOutExpo"));
    }

    private void Start()
    {
        StartCoroutine(TypingText());
    }

    public void AddText(string str)
    {
        _texts.Add(str);
    }

    public void SetCharacter(string characterName)
    {
        _character.sprite = Resources.Load<Sprite>("CharacterImage/" + characterName);
    }

    public void SetName(string newName)
    {
        transform.Find("NameTag").Find("Text").GetComponent<Text>().text = newName;
    }

    private void Touch()
    {
        _isTouch = true;
    }

    private IEnumerator TypingText()
    {
        if (_texts.Count == 0)
            yield break;

        int textsIndex = 0;
        int i = 0;
        string currentText = _texts[textsIndex];
        string outputText = "";

        while (true)
        {
            for (; i < currentText.Length; i++)
            {
                outputText += currentText[i];
                _text.text = outputText;
                yield return new WaitForSeconds(0.05f);

                if (_isTouch)
                {
                    Debug.Log("대화스킵");
                    _isTouch = false;
                    _text.text = currentText;
                    i = int.MaxValue;
                    break;
                }
            }

            yield return null;
            if (_isTouch)
            {
                Debug.Log("다음 문장");
                _isTouch = false;
                i = 0;
                textsIndex++;
                if (textsIndex >= _texts.Count)
                {
                    Debug.Log("대화 끝");
                    Destroy(gameObject);
                    yield break;
                }
                currentText = _texts[textsIndex];
                outputText = string.Empty;
            }
            
        }
    }
}
