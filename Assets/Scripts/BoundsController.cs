using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Barreira"))
        {
            if(gameObject.name.Equals("up"))
                transform.parent.GetComponent<CameraController>().setBound(2);
            if (gameObject.name.Equals("right"))
                transform.parent.GetComponent<CameraController>().setBound(0);
            if (gameObject.name.Equals("left"))
                transform.parent.GetComponent<CameraController>().setBound(1);
            if(gameObject.name.Equals("down"))
                transform.parent.GetComponent<CameraController>().setBound(3);
        }
    }
}
