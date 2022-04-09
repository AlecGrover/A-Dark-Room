using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractSystem : MonoBehaviour
{

    public LayerMask InteractLayerMask = new LayerMask();

    // Start is called before the first frame update
    void Start()
    {
        
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
                }
            }
        }
    }




}
