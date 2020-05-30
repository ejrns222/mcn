using System;
using System.Collections;
using System.Collections.Generic;
using Characters;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Util;

public class CCharacterChanger : MonoBehaviour
{
    public CCharacterSlot slotPrefab;
    private List<GameObject> _slotList = new List<GameObject>(); //현재는 아무곳에도 안 쓰이는 중
    public MonitoringButton clickedButton;
    
    private void OnEnable()
    {
        _slotList.Clear();
        foreach (var v in CInventory.Instance.streamerList)
        {
            GameObject slot;
            slot = Instantiate(slotPrefab.gameObject, GameObject.Find("CharacterChangeSlots").transform);
            _slotList.Add(slot);

            slot.GetComponentInChildren<Text>().text = v.Tag.ToString();
            slot.GetComponent<CCharacterSlot>().streamer = v;
        }
    }

    /// <summary>
    /// @brief : 인벤토리에 있는 스트리머와 장착한 스트리머를 바꾼다
    /// </summary>
    /// <param name="slot"></param>  선택한 슬롯
    public void CharacterChange(GameObject slot)
    {
        int invenIdx = CInventory.Instance.streamerList.IndexOf(slot.GetComponent<CCharacterSlot>().streamer);
        int equipIdx = Player.Instance.equippedStreamers.IndexOf(clickedButton.Streamer);

        if (equipIdx == -1)
        {
            Player.Instance.equippedStreamers.Add(CInventory.Instance.streamerList[invenIdx]);
        }
        else
        {
            var tmp = CInventory.Instance.streamerList[invenIdx];
            CInventory.Instance.streamerList[invenIdx] = Player.Instance.equippedStreamers[equipIdx];
            Player.Instance.equippedStreamers[equipIdx] = tmp;
        }
        
        GameObject.Find("Monitoring").GetComponent<CMonitoring>().Refresh();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// @brief : 장착한 스트리머를 해제한다.
    /// </summary>
    public void CharacterRemove()
    {
        int equipIdx = Player.Instance.equippedStreamers.IndexOf(clickedButton.Streamer);
        CInventory.Instance.streamerList.Add(Player.Instance.equippedStreamers[equipIdx]);
        Player.Instance.equippedStreamers.RemoveAt(equipIdx);
        
        GameObject.Find("Monitoring").GetComponent<CMonitoring>().Refresh();
        gameObject.SetActive(false);
    }
}
