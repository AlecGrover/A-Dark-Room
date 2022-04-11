using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public enum FloorType
{
    Plain,
    Cobblestone,
    Wood
}

[Serializable]
public struct FloorTypeModel
{
    public FloorType TFloorType;
    public GameObject FloorObject;

    public void SetFloorObjectActive(bool active)
    {
        if (FloorObject) FloorObject.SetActive(active);
    }

}

[ExecuteInEditMode]
public class Floor : MonoBehaviour
{

    public FloorType ThisFloorType = FloorType.Plain;
    public List<FloorTypeModel> FloorTypes = new List<FloorTypeModel>(3);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        foreach (var floorType in FloorTypes)
        {
            if (floorType.TFloorType != ThisFloorType) floorType.SetFloorObjectActive(false);
        }
        var activeFloorType = FloorTypes.FirstOrDefault(floorType => floorType.TFloorType == ThisFloorType);
        activeFloorType.SetFloorObjectActive(true);
    }

    public void SetFloorType(FloorType floorType)
    {
        ThisFloorType = floorType;
    }

}
