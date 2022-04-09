using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private Vector3 _playerLocation = Vector3.zero;

    public RoomBuilder StartingRoom;

    // Start is called before the first frame update
    void Start()
    {
        MoveToRoom(StartingRoom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToRoom(RoomBuilder room)
    {
        _playerLocation = room.RoomLocation;
        transform.position = room.transform.position;
    }

}
