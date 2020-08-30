using System;
using System.Collections.Generic;
using Characters;
using UIScripts;
using UnityEngine;
using UnityEngine.UI;

public class CCharacterChanger : MonoBehaviour
{
    [SerializeField] private CCharacterChangeSlot changeSlotPrefab = null;
    [SerializeField] private GameObject changeSlots = null;
    private List<CCharacterChangeSlot> _changeSlotList = new List<CCharacterChangeSlot>(); 
    public CEquipSlot selectedEquipSlot; //모니터링 클래스에서 정해주는 변수임
    private CCharacterChangeSlot _selectedSlot;
    
    private void OnEnable()
    {
        _changeSlotList.Clear();
        foreach (var v in CInventory.streamerList)
        {
            if(v == null)
                continue;
            GameObject slot;
            slot = Instantiate(changeSlotPrefab.gameObject, changeSlots.transform);
            slot.GetComponent<CCharacterChangeSlot>().characterChanger = this;
            _changeSlotList.Add(slot.GetComponent<CCharacterChangeSlot>());

            slot.GetComponentInChildren<Text>().text = v.Tag.ToString();
            slot.GetComponent<CCharacterChangeSlot>().streamer = v;
            slot.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharacterImage/" + v.Tag);
        }
    }

    public void SelectSlot(CCharacterChangeSlot slot)
    {
        _selectedSlot = slot;
        transform.Find("Panel/StreamerInfo/Text").GetComponent<Text>().text = slot.streamer.Info();

        foreach (var v in _changeSlotList)
        {
           v.image.color = Color.white;
        }
        slot.image.color = Color.gray;
    }

    /// <summary>
    /// @brief : 인벤토리에 있는 스트리머와 장착한 스트리머를 바꾼다
    /// </summary>
    public void CharacterChange()
    {
        int invenIdx = CInventory.streamerList.IndexOf(_selectedSlot.streamer);
        int equipIdx = Array.FindIndex(Player.Instance.equippedStreamers, i => i == selectedEquipSlot.streamer);

        //if (equipIdx == -1)
        
        if(selectedEquipSlot.streamer == null)//장착이 안되어 있을 때
        {
            Player.Instance.equippedStreamers[selectedEquipSlot.index] = CInventory.streamerList[invenIdx];
            CInventory.streamerList.RemoveAt(invenIdx);
            selectedEquipSlot.ChangeState(SlotState.OpenEquipped);
        }
        else//이미 장착이 되어 있을 때
        {
            var tmp = CInventory.streamerList[invenIdx];
            CInventory.streamerList[invenIdx] = Player.Instance.equippedStreamers[equipIdx];
            Player.Instance.equippedStreamers[equipIdx] = tmp;
            selectedEquipSlot.ChangeState(SlotState.OpenEquipped);
        }
        
        GameObject.Find("Monitoring").GetComponent<CMonitoring>().Refresh();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// @brief : 장착한 스트리머를 해제한다.
    /// </summary>
    public void CharacterRemove()
    {
        int equipIdx = Array.FindIndex(Player.Instance.equippedStreamers, i => i == selectedEquipSlot.streamer);
        if (Player.Instance.equippedStreamers[equipIdx] == null)
        {
            gameObject.SetActive(false);
            return;
        }

        CInventory.streamerList.Add(Player.Instance.equippedStreamers[equipIdx]);
        Player.Instance.equippedStreamers[equipIdx] = null;
        selectedEquipSlot.ChangeState(SlotState.OpenEmpty);
        
        gameObject.SetActive(false);
    }
}
