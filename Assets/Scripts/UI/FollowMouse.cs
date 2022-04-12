using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FollowMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Camera.current) return;
        var mouseLocation = Camera.current.ScreenToViewportPoint(Input.mousePosition);
        var outOfFrame = Mathf.Min(mouseLocation.x, mouseLocation.y) < 0 ||
                         Mathf.Max(mouseLocation.x, mouseLocation.y) > 1;
        if (!outOfFrame) transform.position = Input.mousePosition;
    }
}
