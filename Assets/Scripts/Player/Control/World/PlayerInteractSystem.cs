using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInteractSystem : MonoBehaviour
{

    public LayerMask InteractLayerMask = new LayerMask();
    private Player _player;



    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            // Debug.Log("Detected player interact attempt");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hitVolume = Physics.Raycast(ray, out hit, 36f, InteractLayerMask);
            if (hitVolume)
            {
                // Debug.Log("Detected interactable object");
                Debug.Log(hit.transform.name);
                Door door = hit.transform.gameObject.GetComponent<Door>();
                if (door)
                {
                    door.AttemptOpen();
                    return;
                }

                Item item = hit.transform.gameObject.GetComponent<Item>();
                if (item)
                {
                    bool added = _player.TryAddToInventory(item);
                    if (added)
                    {
                        item.gameObject.SetActive(false);
                    }
                }

            }
        }
    }




}
