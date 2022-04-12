using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Rooms;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInteractSystem : MonoBehaviour
{

    public LayerMask InteractLayerMask = new LayerMask();
    private Player _player;
    private DialogueSystem _dialogueSystem;



    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
        _dialogueSystem = FindObjectOfType<DialogueSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            // Debug.Log("Detected player interact attempt");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hitVolume = Physics.Raycast(ray, out hit, 45f, InteractLayerMask);
            if (hitVolume)
            {
                // Debug.Log("Detected interactable object");
                Debug.Log(hit.transform.name);
                Lock lockComponent = hit.transform.gameObject.GetComponent<Lock>();
                Door door = hit.transform.gameObject.GetComponent<Door>();
                if (door)
                {
                    door.AttemptOpen();
                    return;
                }
                else if (lockComponent)
                {
                    lockComponent.TryUnlock();
                }

                Item item = hit.transform.gameObject.GetComponent<Item>();
                if (item)
                {
                    bool added = _player.TryAddToInventory(item);
                    if (added)
                    {
                        item.gameObject.SetActive(false);
                        if (_dialogueSystem) _dialogueSystem.TriggerDialogue("You picked up a " + item.InventoryName);
                    }
                }

                ObjectOfInterest objectOfInterest = hit.transform.gameObject.GetComponent<ObjectOfInterest>();
                if (objectOfInterest) objectOfInterest.FlavorDialogue();

                EscapePanelButton escapePanelButton = hit.transform.GetComponent<EscapePanelButton>();
                if (escapePanelButton) escapePanelButton.Interact();

            }
        }
    }




}
