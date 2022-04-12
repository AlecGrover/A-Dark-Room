using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Rooms;
using UnityEngine;

public class ObjectChangeLock : Lock
{

    public GameObject LockedGameObject;
    public GameObject UnlockedGameObject;
    public bool DisableColliderOnUnlock = false;

    protected override void Update()
    {
        base.Update();
        bool unlocked = UnlockItemIDs.Count == 0;
        if (LockedGameObject) LockedGameObject.SetActive(!unlocked);
        if (UnlockedGameObject) UnlockedGameObject.SetActive(unlocked);
        if (DisableColliderOnUnlock) GetComponent<BoxCollider>().enabled = !unlocked;
    }
}
