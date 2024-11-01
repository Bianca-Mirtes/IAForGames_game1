using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform colliders;
    private float maxX, minX;
    private float maxY, minY;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(colliders != null)
        transform.position = new Vector2();
    }

    public float setBound(int num)
    {
        return float.MaxValue;
    }
}
