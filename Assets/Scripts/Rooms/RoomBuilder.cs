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

}

[ExecuteInEditMode]
[Serializable]
public class RoomBuilder : MonoBehaviour
{

    public WallSystem NorthWall;
    public WallSystem SouthWall;
    public WallSystem EastWall;
    public WallSystem WestWall;

    public static Vector3 RoomScale = new Vector3(68f, 32f, 68f);
    public Vector3 RoomLocation = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnValidate()
    {
        NorthWall.SetHasDoor();
        SouthWall.SetHasDoor();
        EastWall.SetHasDoor();
        WestWall.SetHasDoor();

        gameObject.transform.position = ElementWiseMultiplication.Multiply(RoomLocation, RoomScale);
    }


    public void SetDoor(Direction openingDirection, bool doorEnabled)
    {
        switch (openingDirection)
        {
            case Direction.North:
                NorthWall.HasDoor = doorEnabled;
                break;
            case Direction.South:
                SouthWall.HasDoor = doorEnabled;
                break;
            case Direction.West:
                WestWall.HasDoor = doorEnabled;
                break;
            case Direction.East:
                EastWall.HasDoor = doorEnabled;
                break;
        }
    }
}
