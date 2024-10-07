using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int health = 100; // enemy health
    [SerializeField] private int damage; // enemy damage
    [SerializeField] private float timeBtwDamage = 1f; // time for the enemy can to make damage

    private bool doDamage = false;
    private GameObject player;

    private Animator ani; // enemy animator
    private bool isDead; // verif if the enemy is dead
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        damage = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(health < 50)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            damage = 10;
        }
        if(health < 0)
        {
            Destroy(gameObject, 1f);
        }

        if (doDamage)
        {
            health -= player.GetComponent<PlayerController>().GetDamage();
            transform.GetChild(1).GetChild(0).GetComponent<Slider>().value -= player.GetComponent<PlayerController>().GetDamage();
            doDamage = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            doDamage = true;
            Destroy(collision.gameObject);
        }
    }
}
