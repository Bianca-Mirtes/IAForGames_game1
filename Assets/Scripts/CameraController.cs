using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform colliders;
    private float maxX = 0, minX = 0;
    private float maxY = 0, minY = 0;

    // Update is called once per frame
    void Update()
    {

    }

    public void setBound(int id)
    {
        if (id == 0)
            maxX = transform.position.x;
        if(id == 1)
            minX = transform.position.x;
        if (id == 2) 
            maxY = transform.position.y;
        if(id == 3)
            minY = transform.position.y;
    }
}
