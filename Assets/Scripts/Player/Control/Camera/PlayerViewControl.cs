using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PanDirection
{
    Left,
    Right,
    None
}

public class PlayerViewControl : MonoBehaviour
{

    public float DegreesPerSecond = 0.15f;
    private PanDirection _activePanDirection = PanDirection.None;
    private Camera gameCamera;

    void Start()
    {
        gameCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (_activePanDirection == PanDirection.None) return;
        int direction = _activePanDirection == PanDirection.Right ? 1 : -1;
        gameCamera.transform.Rotate(new Vector3(0, direction * DegreesPerSecond * Time.deltaTime, 0), Space.Self);
    }

    public void SetPanDirection(PanDirection panDirection)
    {
        _activePanDirection = panDirection;
    }

}
