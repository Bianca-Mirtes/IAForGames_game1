using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int health = 100; // enemy health
    [SerializeField] private int damage = 5; // enemy damage

    private bool doDamage = false;
    private GameObject player;
    [SerializeField] private bool isDead = false; // verif if the enemy is dead

    private Animator ani; // enemy animator
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
            GetComponent<SpriteRenderer>().color = Color.black;
            damage = 10;
        }
        if(health <= 0)
        {
            isDead = true;
            Destroy(gameObject, 1f);
        }

        if (doDamage)
        {
            health -= player.GetComponent<PlayerController>().GetDamage();
            transform.GetChild(1).GetChild(0).GetComponent<Slider>().value = health;
            doDamage = false;
        }
    }
    public bool GetIsDead() { return isDead; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            doDamage = true;
            Destroy(collision.gameObject);
        }
    }
}
