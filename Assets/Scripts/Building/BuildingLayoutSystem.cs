using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLayoutSystem : MonoBehaviour
{
    // public Room[,] Rooms = new Room[5,5];
    [SerializeField]
    public List<List<Room>> Rooms = new List<List<Room>>();
    public static int RoomScale = 72;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
