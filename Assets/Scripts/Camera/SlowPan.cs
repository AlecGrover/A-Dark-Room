using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPan : MonoBehaviour
{

    public Vector3 RotationsPerSecond = new Vector3(0f, 0.25f, 0f);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotationsPerSecond.x * 360 * Time.deltaTime, RotationsPerSecond.y * 360 * Time.deltaTime, RotationsPerSecond.z * 360 * Time.deltaTime);
    }
}
