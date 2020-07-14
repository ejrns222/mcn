using System;
using System.Collections;
using System.Collections.Generic;
using Characters;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CSummonWindow : MonoBehaviour
{
    public ERank? rank = ERank.A;
    public StreamerBase streamer;
    public Image check1, check2, check3, checkFin, imgBase;
    public Text text;
    public GameObject streamerImg;
    public Button btn;

    private float _timer;
    private bool _bCheck1, _bCheck2, _bCheck3, _bCheckFin, _bAppear;
    private bool _isSkip;
    private bool _isImgLoad;

    private bool _isPhaseEnd;
    private IEnumerator _playingCoroutine = null;

    private void Start()
    {
        switch (rank)
        {
            case ERank.F:
            case ERank.E:
                check1.sprite = Resources.Load<Sprite>("UIImage/Employment/EmpCheck1");
                break;
            
            case ERank.D:
            case ERank.C:
            case ERank.B:
                check1.sprite = Resources.Load<Sprite>("UIImage/Employment/EmpCheck1");
                check2.sprite = Resources.Load<Sprite>("UIImage/Employment/EmpCheck2");
                break;
            
            case ERank.A:
                check1.sprite = Resources.Load<Sprite>("UIImage/Employment/EmpCheck1");
                check2.sprite = Resources.Load<Sprite>("UIImage/Employment/EmpCheck2");
                check3.sprite = Resources.Load<Sprite>("UIImage/Employment/EmpCheck3");
                break;
            
            case null:
                checkFin.sprite = Resources.Load<Sprite>("UIImage/Employment/EmpFail");
                break;
        }
        
        _timer = -0.6f;
        _bCheck1 = _bCheck2 = _bCheck3 = _bCheckFin = _bAppear =false;
        _isSkip =  false;
        _isImgLoad = false;
        _isPhaseEnd = false;
        btn.onClick.AddListener(OnButtonClick);
    }

    public void InitForSpecialGatcha()
    {
        streamerImg.transform.Find("Image").GetComponent<Image>().sprite =
            Resources.Load<Sprite>("CharacterImage/PremiumRandom");
        imgBase.color = new Color(250, 255, 100);


    }

    private void Update()
    {
        bool isEnd = false;
        _timer += Time.deltaTime;
        if (_isSkip)
            _timer = 100;
        if (!_bCheck1 && _timer >= 0)
        {
            if(!check1.gameObject.activeSelf)
                check1.gameObject.SetActive(true);
            float scale = 4 - _timer * 30;
            if (scale < 1)
            {
                scale = 1;
                _bCheck1 = true;
                isEnd = true;
                if(!_isSkip)
                    StartCoroutine(ImgShaking(imgBase.transform, 0.05f, 0.5f, 0.2f));
            }

            check1.transform.localScale = new Vector3(scale,scale);
            var color = check1.color;
            color = new Color(color.r,color.g,color.b,_timer * (1/0.1f));
            check1.color = color;
            if(isEnd && !_isSkip)
                _timer = -0.6f;
        }
        
        if (!_bCheck2 && _timer >= 0 && _bCheck1)
        {
            if(!check2.gameObject.activeSelf)
                check2.gameObject.SetActive(true);
            float scale = 4 - _timer * 30;
            if (scale < 1)
            {
                scale = 1;
                _bCheck2 = true;
                isEnd = true;
                if(!_isSkip)
                    StartCoroutine(ImgShaking(imgBase.transform, 0.05f, 0.3f, 0.2f));
            }

            check2.transform.localScale = new Vector3(scale,scale);
            var color = check2.color;
            color = new Color(color.r,color.g,color.b,_timer * (1/0.1f));
            check2.color = color;
            if(isEnd && !_isSkip)
                _timer = -0.6f;
        }
        
        if (!_bCheck3 && _timer >= 0 && _bCheck2)
        {
            if(!check3.gameObject.activeSelf)
                check3.gameObject.SetActive(true);
            float scale = 4 - _timer * 30;
            if (scale < 1)
            {
                scale = 1;
                _bCheck3 = true;
                isEnd = true;
                if(!_isSkip)
                    StartCoroutine(ImgShaking(imgBase.transform, 0.05f, 0.3f, 0.2f));
            }

            check3.transform.localScale = new Vector3(scale,scale);
            var color = check3.color;
            color = new Color(color.r,color.g,color.b,_timer * (1/0.1f));
            check3.color = color;
            if(isEnd && !_isSkip)
                _timer = -0.6f;
        }
        
        if (!_bCheckFin && _timer >= 0 && _bCheck3)
        {
            if(!checkFin.gameObject.activeSelf)
                checkFin.gameObject.SetActive(true);
            float scale = 4 - _timer * 30;
            if (scale < 1)
            {
                scale = 1;
                _bCheckFin = true;
                isEnd = true;
                
                if(!_isSkip)
                    StartCoroutine(ImgShaking(imgBase.transform, 0.05f, 0.3f, 0.2f));
            }

            checkFin.transform.localScale = new Vector3(scale,scale);
            var color = checkFin.color;
            color = new Color(color.r,color.g,color.b,_timer * (1/0.1f));
            checkFin.color = color;
            if(isEnd && !_isSkip)
                _timer = -1f;
        }

        if (_bCheckFin && _timer >= 0 && !_bAppear)
        {
            if (_isSkip)
            {
                streamerImg.SetActive(true);
                streamerImg.transform.Find("Image").GetComponent<Image>().sprite =
                    Resources.Load<Sprite>("CharacterImage/" + streamer.Tag);
                _isImgLoad = true;
                _isPhaseEnd = true;
                _bAppear = true;
            }
            
            if (!_isImgLoad)
            {
                streamerImg.SetActive(true);
                
                _isImgLoad = true;
                if (!_isSkip)
                {
                    _playingCoroutine = ImgShaking(streamerImg.transform, -0.001f, 0.02f, 2.0f);
                    StartCoroutine(_playingCoroutine);
                }
            }

            var color = streamerImg.transform.Find("Mask").GetComponent<Image>().color;
            if (color.a < 1 && !_isPhaseEnd)
            {
                color.a += Time.deltaTime / 1.5f;
                streamerImg.transform.Find("Mask").GetComponent<Image>().color = color;
                if (color.a >= 1)
                {
                    _isPhaseEnd = true;
                    streamerImg.transform.Find("Image").GetComponent<Image>().sprite =
                        Resources.Load<Sprite>("CharacterImage/" + streamer.Tag);
                    if (!_isSkip)
                    {
                        _timer = -1.5f;
                        _playingCoroutine = ImgShaking(streamerImg.transform, 0.0005f, 0.02f, 1f);
                        StartCoroutine(_playingCoroutine);
                    }
                }
            }

            if (color.a >= 0 && _isPhaseEnd && _timer >= 0)
            {
                color.a -= Time.deltaTime / 0.5f;
                streamerImg.transform.Find("Mask").GetComponent<Image>().color = color;
                btn.transform.GetChild(0).GetComponent<Text>().text = "닫기";
                text.gameObject.SetActive(true);
                text.text = streamer.Tag + "\nRank : " + streamer.Rank;
                if (color.a < 0)
                    _bAppear = true;
            }
        }
        
    }

    private void OnButtonClick()
    {
        if (_bAppear)
        {
            Destroy(gameObject);
            return;
        }

        _isSkip = true;
        if(_playingCoroutine != null)
            StopCoroutine(_playingCoroutine);
        streamerImg.transform.Find("Mask").gameObject.SetActive(false);
        btn.transform.GetChild(0).GetComponent<Text>().text = "닫기";
    }

    private IEnumerator ImgShaking(Transform transform, float decay, float intensity, float time)
    {
        Vector3 originPosition = transform.position;
        Quaternion originRotation = transform.rotation;

        while (time >= 0)
        {
            transform.position = originPosition + Random.insideUnitSphere * intensity;

            yield return new WaitForSeconds(0.02f);
            time -= 0.02f;
            intensity -= decay;
            if (intensity < 0)
                intensity = 0;
        }

        transform.position = originPosition;
    }
}
