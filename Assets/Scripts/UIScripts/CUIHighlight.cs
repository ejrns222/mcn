using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUIHighlight : MonoBehaviour
{
    private RectTransform _left;
    private RectTransform _top;
    private RectTransform _right;
    private RectTransform _bottom;
    
    private RectTransform _leftUp;
    private RectTransform _rightUp;
    private RectTransform _leftBottom;
    private RectTransform _rightBottom;
    
    public RectTransform targetUI;

    private RectTransform _maskButton;
    
    public delegate void HighlightEndHandler();

    private HighlightEndHandler _onHighlightEnd;

    private void Awake()
    {
        _left = transform.GetChild(0).GetComponent<RectTransform>();
        _top = transform.GetChild(1).GetComponent<RectTransform>();
        _right = transform.GetChild(2).GetComponent<RectTransform>();
        _bottom = transform.GetChild(3).GetComponent<RectTransform>();
        
        _leftUp = transform.GetChild(4).GetComponent<RectTransform>();
        _rightUp = transform.GetChild(5).GetComponent<RectTransform>();
        _leftBottom = transform.GetChild(6).GetComponent<RectTransform>();
        _rightBottom = transform.GetChild(7).GetComponent<RectTransform>();

        _maskButton = transform.GetChild(9).GetComponent<RectTransform>();
    }

    public static CUIHighlight CreateHighlight(GameObject target)
    {
        var highlight = Instantiate(Resources.Load("UIPrefabs/HighlightPrefab") as GameObject).GetComponent<CUIHighlight>();
        highlight.targetUI = target.GetComponent<RectTransform>();
        highlight.transform.SetParent(GameObject.Find("Canvas").transform);
        highlight.transform.localPosition = Vector3.zero;
        highlight.transform.localScale = Vector3.one;
        return highlight;
    }

    public void AddOnHighlightEnd(HighlightEndHandler func)
    {
        _onHighlightEnd += func;
    }

    private void Start()
    {
        //TEST
        //_targetUI = GameObject.Find("Menu").transform.Find("Button").GetComponent<RectTransform>();
        //
        //Vector2 targetPos = _targetUI.localPosition;
        Vector2 targetPos = WorldToCanvas(targetUI.gameObject);
        _left.localPosition = new Vector3(targetPos.x - 720,targetPos.y);
        _right.localPosition = new Vector3(targetPos.x + 720,targetPos.y);
        _top.localPosition = new Vector3(targetPos.x,targetPos.y + 1280);
        _bottom.localPosition = new Vector3(targetPos.x,targetPos.y - 1280);
        
        _leftUp.localPosition = new Vector3(_left.localPosition.x,_top.localPosition.y);
        _rightUp.localPosition = new Vector3(_right.localPosition.x,_top.localPosition.y);
        _leftBottom.localPosition = new Vector3(_left.localPosition.x,_bottom.localPosition.y);
        _rightBottom.localPosition = new Vector3(_right.localPosition.x,_bottom.localPosition.y);

        _maskButton.localPosition = WorldToCanvas(targetUI.gameObject);
        _maskButton.sizeDelta = new Vector2(targetUI.rect.width,targetUI.rect.height);
        
        StartCoroutine(MaskShrink());
    }

    private IEnumerator MaskShrink()
    {
        float elapsedTime = 0;
        Vector2 leftPos = _left.localPosition;
        Vector2 topPos = _top.localPosition;
        Vector2 rightPos = _right.localPosition;
        Vector2 bottomPos = _bottom.localPosition;
        //Vector2 targetPos = _targetUI.localPosition;
        Vector2 targetPos = WorldToCanvas(targetUI.gameObject);
        while (true)
        {
            _left.localPosition = new Vector3(Mathf.Lerp(leftPos.x,targetPos.x - targetUI.rect.width/2 - 360,elapsedTime),leftPos.y);
            _top.localPosition = new Vector3(topPos.x,Mathf.Lerp(topPos.y,targetPos.y + targetUI.rect.height/2 + 640,elapsedTime));
            _right.localPosition = new Vector3(Mathf.Lerp(rightPos.x,targetPos.x + targetUI.rect.width/2 + 360,elapsedTime),rightPos.y);
            _bottom.localPosition = new Vector3(bottomPos.x,Mathf.Lerp(bottomPos.y,targetPos.y - targetUI.rect.height/2 - 640,elapsedTime));
            
            _left.sizeDelta = new Vector2(720,Mathf.Lerp(1280,targetUI.rect.height,elapsedTime));
            _right.sizeDelta = new Vector2(720,Mathf.Lerp(1280,targetUI.rect.height,elapsedTime));
            _top.sizeDelta = new Vector2(Mathf.Lerp(720,targetUI.rect.width,elapsedTime),1280);
            _bottom.sizeDelta = new Vector2(Mathf.Lerp(720,targetUI.rect.width,elapsedTime),1280);
            
            _leftUp.localPosition = new Vector3(_left.localPosition.x,_top.localPosition.y);
            _rightUp.localPosition = new Vector3(_right.localPosition.x,_top.localPosition.y);
            _leftBottom.localPosition = new Vector3(_left.localPosition.x,_bottom.localPosition.y);
            _rightBottom.localPosition = new Vector3(_right.localPosition.x,_bottom.localPosition.y);
            
            //_leftUp.sizeDelta = new Vector2(2 * (_top.localPosition.x - _leftUp.localPosition.x - _top.rect.width/2), 2 * (_leftUp.localPosition.y - _left.localPosition.y - _left.rect.height));
            
            yield return new WaitForSeconds(0.02f);
            if (elapsedTime > 1)
            {
                yield return StartCoroutine(HighlightButton());
            }
            elapsedTime += 0.1f;

        }
    }

    private IEnumerator HighlightButton()
    {
        _maskButton.gameObject.SetActive(true);
        var img = _maskButton.GetComponent<Image>();
        bool tnf = true;
        while (true)
        {
            if (tnf)
            {
                img.color = new Color(1,1,1,0.5f);
            }
            else
            {
                img.color = new Color(0.5f,0.5f,0.5f,0.5f);
            }

            tnf = !tnf;

            yield return new WaitForSeconds(0.5f);

            if (!_maskButton.gameObject.activeSelf)
            {
                targetUI.GetComponent<Button>().onClick.Invoke();
                _onHighlightEnd?.Invoke();
                Destroy(gameObject);
                yield break;
            }
        }
    }

    private  Vector2 WorldToCanvas(GameObject WorldObject)
    {
        RectTransform CanvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        Camera camera = GameObject.Find("UI Camera").GetComponent<Camera>();
        Vector2 ViewportPosition=camera.WorldToViewportPoint(WorldObject.transform.position);
        Vector2 WorldObject_ScreenPosition=new Vector2(
            ((ViewportPosition.x*CanvasRect.sizeDelta.x)-(CanvasRect.sizeDelta.x*0.5f)),
            ((ViewportPosition.y*CanvasRect.sizeDelta.y)-(CanvasRect.sizeDelta.y*0.5f)));
 
        //now you can set the position of the ui element
        return WorldObject_ScreenPosition;
    }
    
}
