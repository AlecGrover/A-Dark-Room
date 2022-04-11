using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public List<UIInventorySlot> InventorySlots = new List<UIInventorySlot>();

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            var inventorySlot = _player.TryGetSlot(i);
            if (inventorySlot.HeldItem != null && !inventorySlot.InPlayerHands)
            {
                if (InventorySlots[i].SlotImage.sprite == inventorySlot.HeldItem.InventorySprite) continue;
                Debug.Log("Setting sprite");
                InventorySlots[i].SlotImage.sprite = inventorySlot.HeldItem.InventorySprite;
            }
            else
            {
                InventorySlots[i].SlotImage.sprite = null;
            }
        }
    }


}
