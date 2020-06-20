using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//채용 컨텐츠의 하위 버튼들 관리
public class CEmployment : MonoBehaviour
{
   private readonly List<GameObject> _menuButtons = new List<GameObject>();

   private void Awake()
   {
      var menuButtons = transform.GetChild(0).GetChild(1);
      

      for (int i = 0; i < menuButtons.childCount; i++)
      {
         var v = menuButtons.GetChild(i);
         _menuButtons.Add(v.gameObject);
         v.transform.transform.Find("Content").gameObject.SetActive(false);
      }

      _menuButtons[0].transform.Find("Text").GetComponent<Text>().color = new Color(0, 174 / 255f, 239 / 255f, 1);
      _menuButtons[0].transform.Find("Content").gameObject.SetActive(true);

   }

   private void Start()
   {
      gameObject.SetActive(false);
   }

   public void ButtonClick(Button btn)
   {
      foreach (var v in _menuButtons)
      {
         v.transform.Find("Text").GetComponent<Text>().color = Color.black;
         v.transform.transform.Find("Content").gameObject.SetActive(false);
      }

      btn.transform.Find("Text").GetComponent<Text>().color = new Color(0, 174 / 255f, 239 / 255f, 1);
      btn.transform.Find("Content").gameObject.SetActive(true);
   }
}   
   