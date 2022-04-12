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

        [Header("Dialogue System Parameters")]
        private DialogueSystem _dialogueSystem;

        public string NoKeyDialogue = "A sturdy lock";
        public string JustUnlockedDialogue = "The lock falls open";
        public string UnlockedDialogue = "The lock has opened";


        // Start is called before the first frame update
        void Start()
        {
            _dialogueSystem = FindObjectOfType<DialogueSystem>();
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
            if (UnlockItemIDs.Count == 0)
            {
                if (_dialogueSystem) _dialogueSystem.TriggerDialogue(UnlockedDialogue);
                return;
            }
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
                            if (_dialogueSystem) _dialogueSystem.TriggerDialogue(JustUnlockedDialogue);
                            break;
                        }
                    }
                }
            }
            if (UnlockItemIDs.Count > 0 && _dialogueSystem) _dialogueSystem.TriggerDialogue(NoKeyDialogue);
        }

    }
}
