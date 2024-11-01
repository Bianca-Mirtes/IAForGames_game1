using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            transform.parent.GetComponent<CameraController>();
    }
}
