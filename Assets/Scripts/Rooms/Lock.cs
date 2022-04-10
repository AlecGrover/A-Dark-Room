using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Door))]
public class Lock : MonoBehaviour
{

    public List<Item> UnlockItems;
    public bool ConsumeUnlockItems = true;

    private Door _door;

    // Start is called before the first frame update
    void Start()
    {
        _door = GetComponent<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UnlockItems.Count == 0) _door.Unlock();
    }

    public bool TryUseItem(Item item)
    {
        if (!UnlockItems.Contains(item)) return false;
        UnlockItems.Remove(item);
        return ConsumeUnlockItems;
    }

}
