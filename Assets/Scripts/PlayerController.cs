using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalMove * speed, verticalMove*speed);
        rb.velocity = movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("HealthBox"))
        {
            if(transform.GetChild(2).GetChild(0).GetComponent<Slider>().value != 100)
            {
                transform.GetChild(2).GetChild(0).GetComponent<Slider>().value += 15;
                Destroy(collision.gameObject, 1.2f);

                transform.GetChild(2).GetChild(3).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Health Box collected";
                transform.GetChild(2).GetChild(3).GetComponent<FadeController>().FadeIn();
                transform.GetChild(2).GetChild(3).GetComponent<FadeController>().FadeOutWithTime(2f);
            }
        }
        if (collision.gameObject.tag.Equals("AmmoBox"))
        {
            GunController gunScript = transform.GetChild(1).GetComponent<GunController>();
            if(gunScript != null)
            {
                if(gunScript.GetCurrentBullets() < gunScript.GetCapacity())
                {
                    transform.GetChild(1).GetComponent<GunController>().SetCurrentBullets(5);
                    Destroy(collision.gameObject, 1.2f);

                    transform.GetChild(2).GetChild(3).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Ammo Box collected";
                    transform.GetChild(2).GetChild(3).GetComponent<FadeController>().FadeIn();
                    transform.GetChild(2).GetChild(3).GetComponent<FadeController>().FadeOutWithTime(2f);
                }
            }
        }
    }
}
