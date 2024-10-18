using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform player;
    [SerializeField] private float bulletForce;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
        Vector3 direction = player.position - transform.position;
        Vector3 rotation = transform.position - player.position;

        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletForce;

        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rot));
    }
}
