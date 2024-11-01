using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public int health {get; set; } // enemy health
    [SerializeField] public int damage { get; set; } // enemy damage
    [SerializeField] private EnemyGunController gun;

    private GameObject player;
    [SerializeField] private bool isDead = false; // verif if the enemy is dead

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        player = GameObject.FindWithTag("Player");
        damage = 5;
    }

    public void Shoot()
    {
        if (gun.CanShoot())
            gun.Shoot();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            health -= player.GetComponent<PlayerController>().GetDamage();
            transform.GetChild(1).GetChild(0).GetComponent<Slider>().value = health;
            Destroy(collision.gameObject);
        }
    }
}
