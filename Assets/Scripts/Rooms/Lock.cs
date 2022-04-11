using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lock : MonoBehaviour
{

    public List<float> UnlockItemIDs;
    public bool ConsumeUnlockItems = true;

    public Door LockedDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!LockedDoor) return;
        if (UnlockItemIDs.Count == 0) LockedDoor.Unlock();
    }

    public bool TryUseItem(Item item)
    {
        Debug.Log("Trying to use item with ID: " + item.ID);
        if (!UnlockItemIDs.Contains(item.ID)) return false;
        UnlockItemIDs.Remove(item.ID);
        return ConsumeUnlockItems;
    }

}
