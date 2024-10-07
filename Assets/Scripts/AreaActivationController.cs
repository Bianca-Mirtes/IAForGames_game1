using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaActivationController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (!transform.parent.GetComponent<TileController>().PlayerIsHere())
            {
                transform.parent.GetComponent<TileController>().SetAreaActivation(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            transform.parent.GetComponent<TileController>().SetAreaActivation(false);
        }
    }
}
