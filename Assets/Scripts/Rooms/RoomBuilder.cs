using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Math;
using UnityEngine;

[Serializable]
public struct WallSystem
{

    public Wall wall;
    public Door door;
    public bool HasDoor;

    public void SetHasDoor()
    {
        if (wall) wall.SetHasDoor(HasDoor);
        if (door) door.SetDoorEnabled(HasDoor);
    }

    public void SetWallType(WallType wallType)
    {
        if (wall) wall.SetWallType(wallType);
    }

}

[ExecuteInEditMode]
[Serializable]
public class RoomBuilder : MonoBehaviour
{

    public bool TriggerUpdate = false;

    public WallSystem NorthWall;
    public WallSystem SouthWall;
    public WallSystem EastWall;
    public WallSystem WestWall;

    public Floor FloorComponent;

    public static Vector3 RoomScale = new Vector3(68f, 32f, 68f);
    public Vector3 RoomLocation = Vector3.zero;

    public WallType RoomWallType = WallType.Plain;
    public FloorType RoomFloorType = FloorType.Plain;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnValidate()
    {
        if (!Application.isEditor) return;
        NorthWall.SetHasDoor();
        SouthWall.SetHasDoor();
        EastWall.SetHasDoor();
        WestWall.SetHasDoor();

        NorthWall.SetWallType(RoomWallType);
        SouthWall.SetWallType(RoomWallType);
        EastWall.SetWallType(RoomWallType);
        WestWall.SetWallType(RoomWallType);

        FloorComponent.SetFloorType(RoomFloorType);

        gameObject.transform.position = Vector3MathExtension.Multiply(RoomLocation, RoomScale);

        transform.name = string.Format("Room ({0}, {1})", (int) RoomLocation.x, (int) RoomLocation.z);
    }


    public void SetDoor(Direction openingDirection, bool doorEnabled)
    {
        switch (openingDirection)
        {
            case Direction.North:
                NorthWall.HasDoor = doorEnabled;
                NorthWall.SetHasDoor();
                break;
            case Direction.South:
                SouthWall.HasDoor = doorEnabled;
                SouthWall.SetHasDoor();
                break;
            case Direction.West:
                WestWall.HasDoor = doorEnabled;
                WestWall.SetHasDoor();
                break;
            case Direction.East:
                EastWall.HasDoor = doorEnabled;
                EastWall.SetHasDoor();
                break;
        }

        TriggerUpdate = !TriggerUpdate;
    }
}
