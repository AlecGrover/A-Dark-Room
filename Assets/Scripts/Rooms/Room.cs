using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    Spawn,
    Hazard,
    Object,
    Puzzle,
    Exit
}


public class Room : MonoBehaviour
{
    [SerializeField] private Room _northRoom;
    [SerializeField] private Room _southRoom;
    [SerializeField] private Room _westRoom;
    [SerializeField] private Room _eastRoom;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
