using System;
using System.Collections.Generic;
using UIScripts;
using UnityEngine;
using UnityEngine.UI;

public class CCharacterChanger : MonoBehaviour
{
    [SerializeField] private CCharacterChangeSlot changeSlotPrefab = null;
    [SerializeField] private GameObject changeSlots = null;
    private List<GameObject> _changeSlotList = new List<GameObject>(); //현재는 아무곳에도 안 쓰이는 중 캐릭터 선택시 ui변경 예정(선택시 팝업창 생성하고 거기에 정보가 나타나도록)
    public CEquipSlot selectedEquipSlot;
    
    private void OnEnable()
    {
        _changeSlotList.Clear();
        foreach (var v in CInventory.Instance.streamerList)
        {
            GameObject slot;
            slot = Instantiate(changeSlotPrefab.gameObject, changeSlots.transform);
            slot.GetComponent<CCharacterChangeSlot>().characterChanger = this;
            _changeSlotList.Add(slot);

            slot.GetComponentInChildren<Text>().text = v.Tag.ToString();
            slot.GetComponent<CCharacterChangeSlot>().streamer = v;
            slot.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharacterImage/" + v.Tag);
        }
    }

    /// <summary>
    /// @brief : 인벤토리에 있는 스트리머와 장착한 스트리머를 바꾼다
    /// </summary>
    /// <param name="slot"></param>  선택한 슬롯
    public void CharacterChange(CCharacterChangeSlot slot)
    {
        int invenIdx = CInventory.Instance.streamerList.IndexOf(slot.streamer);
        int equipIdx = Array.FindIndex(Player.Instance.equippedStreamers, i => i == selectedEquipSlot.streamer);

        //if (equipIdx == -1)
        
        if(selectedEquipSlot.streamer == null)//장착이 안되어 있을 때
        {
            Player.Instance.equippedStreamers[selectedEquipSlot.index] = CInventory.Instance.streamerList[invenIdx];
            CInventory.Instance.streamerList.RemoveAt(invenIdx);
            selectedEquipSlot.ChangeState(SlotState.OpenEquipped);
        }
        else//이미 장착이 되어 있을 때
        {
            var tmp = CInventory.Instance.streamerList[invenIdx];
            CInventory.Instance.streamerList[invenIdx] = Player.Instance.equippedStreamers[equipIdx];
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

        CInventory.Instance.streamerList.Add(Player.Instance.equippedStreamers[equipIdx]);
        Player.Instance.equippedStreamers[equipIdx] = null;
        selectedEquipSlot.ChangeState(SlotState.OpenEmpty);
        
        gameObject.SetActive(false);
    }
}
