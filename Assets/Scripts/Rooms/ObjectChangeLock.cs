using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChangeLock : Lock
{

    public GameObject LockedGameObject;
    public GameObject UnlockedGameObject;

    protected override void Update()
    {
        base.Update();
        bool unlocked = UnlockItemIDs.Count == 0;
        LockedGameObject.SetActive(!unlocked);
        UnlockedGameObject.SetActive(unlocked);
    }
}
