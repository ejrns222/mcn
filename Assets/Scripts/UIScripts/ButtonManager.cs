using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private Button[] _childButton;
    [SerializeField] GameObject room = null;
    [SerializeField] GameObject bg = null;
    [SerializeField] GameObject portal = null;

    private IEnumerator _moveToPortal = null;
    private IEnumerator _moveToHome = null;
    void Awake()
    {
        _childButton = gameObject.GetComponentsInChildren<Button>();
        _moveToPortal = MoveToPortal();
        _moveToHome = MoveToHome();
    }

    /// <summary>
    /// 버튼을 누르면 해당 버튼만 색상 변경
    /// </summary>
    /// <param name="num">버튼의 번호 버튼은 1번부터임</param>
    public void SetButtonColor(int num)
    {
        if (num >= 6)
        {
            room.SetActive(false);
            bg.SetActive(false);
            portal.SetActive(true);
        }
        else
        {
            
            room.SetActive(true);
            bg.SetActive(true);
            portal.SetActive(false);
        }
    }

    IEnumerator MoveToPortal()
    {
        var roomImgs = room.GetComponentsInChildren<Image>();
        var portalImg = portal.GetComponentInChildren<SpriteRenderer>();
        var portalAnim = portal.GetComponentInChildren<SpriteRenderer>();
        var colorWhite = Color.white;

        foreach (var v in roomImgs)
        {
            v.color = Color.white;
        }

        portalAnim.color = Color.white;
        portalImg.color = Color.white;
        
        while (room.activeSelf)
        {
            foreach (var v in roomImgs)
            {
                colorWhite.a -= 0.02f;
                v.color = colorWhite;
                bg.GetComponent<SpriteRenderer>().color = colorWhite;
            }

            if (colorWhite.a < 0)
            {
                room.SetActive(false);
                bg.SetActive(false);
                portal.SetActive(true);
                portalImg.color = colorWhite;
            }

            yield return new WaitForSeconds(0.02f);
        }

        while (portalImg.color.a < 1)
        {
            
            portalAnim.color = colorWhite;
            portalImg.color = colorWhite;
            colorWhite.a += 0.02f;
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator MoveToHome()
    {
        var roomImgs = room.GetComponentsInChildren<Image>();
        var portalImg = portal.GetComponentInChildren<SpriteRenderer>();
        var portalAnim = portal.GetComponentInChildren<SpriteRenderer>();
        var colorWhite = Color.white;
        
        foreach (var v in roomImgs)
        {
            v.color = Color.white;
        }

        portalAnim.color = Color.white;
        portalImg.color = Color.white;
        
        while (portal.activeSelf)
        {
            portalAnim.color = portalImg.color = colorWhite;
            colorWhite.a -= 0.02f;
            yield return new WaitForSeconds(0.02f);
            
            if (colorWhite.a <= 0)
            {
                room.SetActive(true);
                bg.SetActive(true);
                portal.SetActive(false);
                roomImgs[0].color = colorWhite;
            }
        }
        
        while (roomImgs[0].color.a < 1)
        {
            foreach (var v in roomImgs)
            {
                colorWhite.a += 0.02f;
                v.color = colorWhite;
                bg.GetComponent<SpriteRenderer>().color = colorWhite;
            }
            yield return new WaitForSeconds(0.02f);
        }

    }
}
