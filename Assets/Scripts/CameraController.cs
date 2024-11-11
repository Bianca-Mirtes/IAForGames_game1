using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float maxX = 999, minX = -999;
    private float maxY = 999, minY = -999;
    Transform player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            player.position = new Vector3(Mathf.Clamp(player.position.x, minX, maxX), Mathf.Clamp(player.position.x, minY, maxY), player.position.z);
        }
    }

    public void setBound(int id)
    {
        if (id == 0)
            maxX = player.position.x;
        if(id == 1)
            minX = player.position.x;
        if (id == 2) 
            maxY = player.position.y;
        if(id == 3)
            minY = player.position.y;
    }
}
