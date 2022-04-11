using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


[Serializable]
public enum Direction
{
    North,
    South,
    East,
    West
}

[Serializable]
public enum WallType
{
    Plain,
    Cobblestone,
    Ornate,
    StoneBrick
}

[Serializable]
public struct WallTypeModel
{
    public WallType TWallType;
    public GameObject EmptyWallObject;
    public GameObject DoorWallObject;

    public void SetWallObjectsActive(bool active)
    {
        if (EmptyWallObject) EmptyWallObject.SetActive(active);
        if (DoorWallObject) DoorWallObject.SetActive(active);
    }

}


[ExecuteInEditMode]
public class Wall : MonoBehaviour
{
    public static int DirectionalOffset = 33;
    public static int Height = 30;
    public static int BlankWallHeightOffset = 1;
    public Direction WallDirection = Direction.North;

    public GameObject DoorWallGameObject = null;
    public GameObject BlankWallGameObject = null;

    public List<WallTypeModel> WallTypes = new List<WallTypeModel>(4);
    public WallType ThisWallType = WallType.Plain;

    public bool HasDoor = false;

    void OnValidate()
    {

        gameObject.transform.localPosition = Vector3.zero;

        if (BlankWallGameObject)
        {
            PositionWallGameObjects(BlankWallGameObject);
            BlankWallGameObject.transform.localPosition += Vector3.up * (Height + BlankWallHeightOffset);
            // BlankWallGameObject.SetActive(!HasDoor);
        }

        if (DoorWallGameObject)
        {
            PositionWallGameObjects(DoorWallGameObject);
            DoorWallGameObject.transform.localPosition += Vector3.up * (Height);
            // DoorWallGameObject.SetActive(HasDoor);
        }
    }

    void Update()
    {
        if (BlankWallGameObject) BlankWallGameObject.SetActive(!HasDoor);
        if (DoorWallGameObject) DoorWallGameObject.SetActive(HasDoor);

        foreach (var wallType in WallTypes)
        {
            if (wallType.TWallType != ThisWallType) wallType.SetWallObjectsActive(false);
        }
        var activeWallType = WallTypes.FirstOrDefault(wallType => wallType.TWallType == ThisWallType);
        activeWallType.SetWallObjectsActive(true);
    }

    private void PositionWallGameObjects(GameObject wallGameObject)
    {
        switch (WallDirection)
        {
            case Direction.North:
                wallGameObject.transform.localPosition = Vector3.forward * DirectionalOffset;
                wallGameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
                break;
            case Direction.South:
                wallGameObject.transform.localPosition = Vector3.back * DirectionalOffset;
                wallGameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
                break;
            case Direction.East:
                wallGameObject.transform.localPosition = Vector3.right * DirectionalOffset;
                wallGameObject.transform.localRotation = Quaternion.Euler(Vector3.up * 90f);
                break;
            case Direction.West:
                wallGameObject.transform.localPosition = Vector3.left * DirectionalOffset;
                wallGameObject.transform.localRotation = Quaternion.Euler(Vector3.up * 90f);
                break;
        }
    }

    public void SetHasDoor(bool hasDoor)
    {
        HasDoor = hasDoor;
        // if (BlankWallGameObject) BlankWallGameObject.SetActive(!HasDoor);
        // if (DoorWallGameObject) DoorWallGameObject.SetActive(HasDoor);
    }

    public void SetWallType(WallType wallType)
    {
        ThisWallType = wallType;
    }

}
