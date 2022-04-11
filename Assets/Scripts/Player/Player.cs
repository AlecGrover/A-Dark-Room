using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class InventorySlot
{
    public Item HeldItem;
    public string SlotLabel;
    public bool InPlayerHands = false;


    public InventorySlot()
    {

    }
    public InventorySlot(Item item, string label)
    {
        HeldItem = item;
        SlotLabel = label;
        InPlayerHands = false;
    }

    public bool TryAddItem(Item item)
    {
        bool addedItem = false;
        if (HeldItem == null)
        {
            HeldItem = item;
            Debug.Log("Added item to slot");
            addedItem = true;
        }
        return addedItem;
    }

    public bool HasItem()
    {
        return HeldItem != null;
    }

}
public class Player : MonoBehaviour
{

    [Header("Player Location Parameters")]
    [SerializeField]
    private Vector3 _playerLocation = Vector3.zero;
    public RoomBuilder StartingRoom;

    [Header("Player Inventory")]
    public InventorySlot InventorySlot_1 = new InventorySlot(null, "1");
    public InventorySlot InventorySlot_2 = new InventorySlot(null, "2");
    public InventorySlot InventorySlot_3 = new InventorySlot(null, "3");
    public InventorySlot InventorySlot_4 = new InventorySlot(null, "4");
    [SerializeField]
    private List<InventorySlot> _inventorySlots = new List<InventorySlot>();

    void Awake()
    {
        _inventorySlots.Add(InventorySlot_1);
        _inventorySlots.Add(InventorySlot_2);
        _inventorySlots.Add(InventorySlot_3);
        _inventorySlots.Add(InventorySlot_4);
    }

    // Start is called before the first frame update
    void Start()
    {
        MoveToRoom(StartingRoom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToRoom(RoomBuilder room)
    {
        _playerLocation = room.RoomLocation;
        transform.position = room.transform.position;
    }

    public bool TryAddToInventory(Item newItem)
    {
        return _inventorySlots.Any(slot => slot.TryAddItem(newItem));
    }

    public InventorySlot TryGetSlot(int index)
    {
        if (index < _inventorySlots.Count)
        {
            return _inventorySlots[index];
        }

        return null;
    }

    public Sprite TryGetHeldSprite()
    {
        return (from inventoryItem in _inventorySlots where inventoryItem.InPlayerHands select inventoryItem.HeldItem.InventorySprite).FirstOrDefault();
    }

    public List<Item> GetInventoryItems()
    {
        List<Item> returnList = new List<Item>();
        foreach (var inventorySlot in _inventorySlots) returnList.Add(inventorySlot.HeldItem);
        return returnList;
    }

    public void ConsumeItem(Item item)
    {
        if (_inventorySlots.Exists(inventorySlot => inventorySlot.HeldItem == item))
        {
            var slotToRemove = _inventorySlots.First(invSlot => invSlot.HeldItem == item);
            slotToRemove.HeldItem = null;
            slotToRemove.InPlayerHands = false;
        }
    }

}
