using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Rooms
{
    public class Lock : MonoBehaviour
    {

        public List<float> UnlockItemIDs;
        public bool ConsumeUnlockItems = true;
        public bool DisableLockOnUnlock = false;

        public Door LockedDoor;

        [Header("Sound FX Parameters")]
        public AudioClip OpeningSound;
        public OneShotPlayer SoundSource;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (LockedDoor != null && UnlockItemIDs.Count == 0)
            {
                LockedDoor.Unlock();
                gameObject.SetActive(!DisableLockOnUnlock);
            }
        }

        public bool TryUseItem(Item item)
        {
            Debug.Log("Trying to use item with ID: " + item.ID);
            if (!UnlockItemIDs.Contains(item.ID)) return false;
            UnlockItemIDs.Remove(item.ID);
            return ConsumeUnlockItems;
        }

        public void TryUnlock()
        {
            Player player = FindObjectOfType<Player>();
            if (player)
            {
                foreach (var item in player.GetInventoryItems())
                {
                    if (item)
                    {
                        bool usedItem = TryUseItem(item);
                        if (usedItem)
                        {
                            player.ConsumeItem(item);
                        }

                        if (UnlockItemIDs.Count == 0)
                        {
                            if (SoundSource && OpeningSound) SoundSource.PlayOneShot(OpeningSound);
                            break;
                        }
                    }
                }
            }
        }

    }
}
