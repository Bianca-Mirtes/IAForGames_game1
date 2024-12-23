using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damage = 15;
    [SerializeField] private TileController currentArea;
    public GameObject finalCanva;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        finalCanva = transform.GetChild(3).gameObject;
        finalCanva.SetActive(false);
        finalCanva.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(FindFirstObjectByType<GameManager>().Reiniciar);
        finalCanva.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(FindFirstObjectByType<GameManager>().Exit);
    }

    private void Update()
    {
        if (currentArea != null) { currentArea.SetNeighborsArea(true); }

        float health = transform.GetChild(2).GetChild(1).GetComponent<Slider>().value;

        if (health <= 0)
        {
            finalCanva.transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = Color.red;
            finalCanva.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Game Over";
            finalCanva.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public int GetDamage() { return damage; }

    void FixedUpdate()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalMove * speed, verticalMove*speed);
        rb.velocity = movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
            currentArea = collision.gameObject.GetComponent<TileController>();

        if (collision.gameObject.tag.Equals("HealthBox"))
        {
            if(transform.GetChild(2).GetChild(1).GetComponent<Slider>().value != 100)
            {
                transform.GetChild(2).GetChild(1).GetComponent<Slider>().value += 20;
                currentArea.RemoveBuffer(gameObject);
                Destroy(collision.gameObject, 1.1f);

                transform.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Health Box collected";
                transform.GetChild(2).GetChild(4).GetComponent<FadeController>().FadeInForFadeOut(2f);
            }
        }
        if (collision.gameObject.tag.Equals("AmmoBox"))
        {
            GunController gunScript = transform.GetChild(1).GetComponent<GunController>();
            if(gunScript != null)
            {
                if(gunScript.GetCurrentBullets() < gunScript.GetCapacity())
                {
                    transform.GetChild(1).GetComponent<GunController>().SetCurrentBullets(10);
                    currentArea.RemoveBuffer(gameObject);
                    Destroy(collision.gameObject, 1.1f);

                    transform.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Ammo Box collected";
                    transform.GetChild(2).GetChild(4).GetComponent<FadeController>().FadeInForFadeOut(2f);
                }
            }
        }
    }

    public TileController getCurrentArea()
    {
        return currentArea;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            transform.GetChild(2).GetChild(1).GetComponent<Slider>().value -= 5;
            Destroy(collision.gameObject);
        }
    }
}
