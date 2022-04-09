using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Door : MonoBehaviour
{
    private const int RoomCount = 2;
    public bool DoorEnabled = false;
    public bool Open = false;
    [Tooltip("The first room is always the room that must be exited, i.e. the room the door opens from")]
    public List<RoomBuilder> Rooms = new List<RoomBuilder>(RoomCount);
    public Direction OpeningDirection = Direction.West;
    public Vector3 LocationOffset = Vector3.zero;
    public float BaseRotation = 90f;
    public float OpenAngle = 120;
    public GameObject DoorGameObject;


    void OnValidate()
    {
        if (Rooms.Count < 2) return;
        float adjustedAngle = Open ? BaseRotation + OpenAngle : BaseRotation;
        switch (OpeningDirection)
        {
            case Direction.East:
                transform.rotation = Quaternion.Euler(0, 90 + adjustedAngle, 0);
                break;
            case Direction.West:
                transform.rotation = Quaternion.Euler(0, 270 + adjustedAngle, 0);
                break;
            case Direction.South:
                transform.rotation = Quaternion.Euler(0, 180 + adjustedAngle, 0);
                break;
            case Direction.North:
                transform.rotation = Quaternion.Euler(0, 0 + adjustedAngle, 0);
                break;
        }
        if (Rooms[0] && Rooms[1] && DoorGameObject) DoorGameObject.transform.position = (Rooms[0].transform.position + Rooms[1].transform.position) / 2f + LocationOffset;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DoorGameObject) DoorGameObject.SetActive(DoorEnabled);
        if (Rooms.Count < 2) return;
        if (Rooms[0]) Rooms[0].SetDoor(OpeningDirection, DoorEnabled);
        if (Rooms[1]) Rooms[1].SetDoor(GetOpposingDirection(), DoorEnabled);
    }

    public void SetDoorEnabled(bool hasDoor)
    {
        DoorEnabled = hasDoor;
    }

    private Direction GetOpposingDirection()
    {
        Direction opposingDirection = OpeningDirection switch
        {
            Direction.South => Direction.North,
            Direction.North => Direction.South,
            Direction.West => Direction.East,
            Direction.East => Direction.West,
            _ => throw new ArgumentOutOfRangeException()
        };

        return opposingDirection;
    }

}
