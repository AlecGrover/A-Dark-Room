using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRotationRegion : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private PlayerViewControl viewControl;
    public PanDirection RegionPanDirection = PanDirection.None;

    // Start is called before the first frame update
    void Start()
    {
        viewControl = transform.parent.gameObject.GetComponent<PlayerViewControl>();
        if (!viewControl)
        {
            Destroy(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        viewControl.SetPanDirection(RegionPanDirection);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        viewControl.SetPanDirection(PanDirection.None);
    }
}
