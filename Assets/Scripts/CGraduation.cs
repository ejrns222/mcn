using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wealths;

public class CGraduation : MonoBehaviour
{
    [SerializeField] private GameObject graduateSlotPrefab = null;
    [SerializeField] private Transform slotsTransform = null;
    private List<CGraduateSlot> _slots;

    private void Awake()
    {
        _slots = new List<CGraduateSlot>();
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        _slots.Clear();
        foreach (var v in CInventory.streamerList)
        {
            if(v == null)
                continue;

            if (v.Subscribers >= v.Expectation)
            {
                GameObject slot = Instantiate(graduateSlotPrefab, slotsTransform);
                slot.GetComponent<CGraduateSlot>().streamer = v;
                _slots.Add(slot.GetComponent<CGraduateSlot>());
            }
        }
    }

    public void AllSelect()
    {
        foreach (var v in _slots)
        {
            v.isSelected = false;
            v.OnClick();
        }
    }

    //TODO : 나중에 태블릿에 내려가고 선택한 스트리머들이 차원문으로 들어가서 사라지는 애니메이션을 만들도록 하자
    public void Graduation()
    {
        long resultPrice = 0;
        foreach (var v in _slots)
        {
            if (v.isSelected)
            {
                v.Graduation();
                resultPrice += v.resultPrice;
            }
        }
        CWealthRenderer.RenderEarnedWealth(resultPrice,EWealth.Jewel);
    }
}
