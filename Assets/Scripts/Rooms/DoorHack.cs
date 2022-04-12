using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHack : Door
{
    [Header("Door Hack Parameters")]
    public GameObject LockedGameObject;
    public GameObject UnlockedGameObject;
    public bool DeactivateColliderOnUnlock = false;

    protected override void Update()
    {
        if (LockedGameObject) LockedGameObject.SetActive(Locked);
        if (UnlockedGameObject) UnlockedGameObject.SetActive(!Locked);
        if (!Locked) _collider.enabled = !DeactivateColliderOnUnlock;
        if (!Locked && !Open)
        {
            if (SoundSource && OpeningSound) SoundSource.PlayOneShot(OpeningSound);
            Open = true;
        }
    }

    public override void Unlock()
    {
        base.Unlock();
    }
}
