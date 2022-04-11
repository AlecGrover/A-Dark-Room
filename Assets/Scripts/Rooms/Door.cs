using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Math;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
public class Door : MonoBehaviour
{
    [Header("Door State")]
    private const int RoomCount = 2;
    public bool DoorEnabled = false;
    public bool Open = false;
    public bool Locked = false;
    public Lock DoorLock;
    public bool HideDoorModel = false;

    [Header("Collider Parameters")]
    private BoxCollider _collider;
    public float ColliderWidth = 4;
    public float ColliderHeight = 10;
    public Vector3 ColliderOffset = Vector3.zero;
    public Vector3 ColliderSize = Vector3.one;
    public bool ColliderValuesOverride = false;

    [Header("Door Transform Parameters")]
    [Tooltip("The first room is always the room that must be exited, i.e. the room the door opens from")]
    public List<RoomBuilder> Rooms = new List<RoomBuilder>(RoomCount);
    public Direction OpeningDirection = Direction.West;
    public Vector3 LocationOffset = Vector3.zero;
    public float BaseRotation = 90f;
    private float _doorLocalBaseRotation = 0f;
    public float OpenAngle = 120;

    [Header("Door Mesh")]
    public GameObject DoorGameObject;


    void OnValidate()
    {
        if (Rooms.Count < 2) return;

        if (Rooms[0]) transform.name = string.Format("{0} [{1}]", Rooms[0].transform.name, OpeningDirection.ToString());

        float adjustedAngle = Open ? BaseRotation + OpenAngle : BaseRotation;
        switch (OpeningDirection)
        {
            case Direction.East:
                _doorLocalBaseRotation = 90f;
                BaseRotation = -270f;
                LocationOffset = new Vector3(-1, 30, 4);
                break;
            case Direction.West:
                _doorLocalBaseRotation = 270f;
                BaseRotation = -90f;
                LocationOffset = new Vector3(1, 30, -4);
                break;
            case Direction.South:
                _doorLocalBaseRotation = 180f;
                BaseRotation = -180f;
                LocationOffset = new Vector3(4, 30, 1);
                break;
            case Direction.North:
                _doorLocalBaseRotation = 0f;
                LocationOffset = new Vector3(-4, 30, -1);
                break;
        }

        DoorGameObject.transform.localRotation = Quaternion.Euler(0, _doorLocalBaseRotation, 0);

        if (DoorGameObject)
        {
            DoorGameObject.transform.localRotation =
                Open ? Quaternion.Euler(0, OpenAngle + _doorLocalBaseRotation, 0) : Quaternion.Euler(0, _doorLocalBaseRotation, 0);
        } 

        
    }


    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DoorGameObject) DoorGameObject.SetActive(DoorEnabled && !HideDoorModel);
        if (_collider) _collider.enabled = DoorEnabled;
        if (Rooms.Count < 2) return;
        if (Rooms[0]) Rooms[0].SetDoor(OpeningDirection, DoorEnabled);
        if (Rooms[1]) Rooms[1].SetDoor(GetOpposingDirection(), DoorEnabled);
        if (Rooms[0] && Rooms[1] && DoorGameObject)
        {
            DoorGameObject.transform.position = (Rooms[0].transform.position + Rooms[1].transform.position) / 2f + LocationOffset;
            if (!_collider) _collider = GetComponent<BoxCollider>();
            if (!ColliderValuesOverride)
            {
                var rawColliderCenter = (Rooms[0].transform.position + Rooms[1].transform.position) / 2f + ColliderOffset;
                _collider.center = Vector3MathExtension.Rotate3DPointAroundYAxis(rawColliderCenter, Mathf.Deg2Rad * transform.rotation.y);
                _collider.size = Vector3MathExtension.Abs(Vector3MathExtension.Rotate3DPointAroundYAxis(ColliderSize, Mathf.Deg2Rad * BaseRotation));
            }
        }
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

    public void AttemptOpen()
    {
        if (Open)
        {
            Player player = FindObjectOfType<Player>();
            if (player && DoorGameObject)
            {
                Debug.Log("Player Forward: " + (DoorGameObject.transform.position - player.transform.position).ToString());
                player.MoveToRoom(GetRoomInDirection(DoorGameObject.transform.position - player.transform.position));
            }
        }
        else
        {
            if (!Locked) Open = true;
            else
            {
                Player player = FindObjectOfType<Player>();
                if (player && DoorLock)
                {
                    foreach (var item in player.GetInventoryItems())
                    {
                        if (item && DoorLock.TryUseItem(item)) player.ConsumeItem(item);
                        if (!Locked) break;
                    }
                }
            }
            if (DoorGameObject)
            {
                DoorGameObject.transform.localRotation =
                    Open ? Quaternion.Euler(0, OpenAngle + _doorLocalBaseRotation, 0) : Quaternion.Euler(0, _doorLocalBaseRotation, 0);
            }
        }
    }

    // Takes the dot product of a provided direction and the direction vector to the exit room (Rooms[1]) to test directionality.
    // If positive, returns the exit room, if negative, returns the origin room
    public RoomBuilder GetRoomInDirection(Vector3 transformForward)
    {
        if (Rooms.Count < 2 || !Rooms[0] || !Rooms[1]) return null;

        float directionOfExitRoom = Vector3.Dot(Rooms[1].transform.position - transform.GetChild(0).position, transformForward);
        Debug.Log("Parallelism of Exit Room: " + directionOfExitRoom);
        RoomBuilder nextRoom;
        if (directionOfExitRoom > 0)
        {
            nextRoom = Rooms[1];
        }
        else
        {
            nextRoom = Rooms[0];
        }

        return nextRoom;
    }

    public void Unlock()
    {
        Locked = false;
    }


}
