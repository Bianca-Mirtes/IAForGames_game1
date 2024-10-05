using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int health = 100; // enemy health
    [SerializeField] private int damage; // enemy damage
    [SerializeField] private float timeBtwDamage = 1f; // time for the enemy can to make damage

    private Animator ani; // enemy animator
    private bool isDead; // verif if the enemy is dead
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        damage = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(health < 50)
        {
            ani.SetTrigger("stateTwo");
            GetComponent<SpriteRenderer>().color = Color.red;
            damage = 10;
        }
        if(health < 0)
        {
            ani.SetTrigger("death");
        }
    }
}
