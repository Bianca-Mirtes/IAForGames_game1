using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyGunController : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int currentBullets;
    [SerializeField] private int capacity = 20;
    [SerializeField] private bool canShoot;
    [SerializeField] private float timeBtwShooting = 1f;

    private Transform player;

    [Header("GameObjects")]
    public GameObject bulletPrefab;
    public Transform gunBarrel;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        currentBullets = capacity;
    }

    // Update is called once per frame
    void Update()
    {
        if (!transform.parent.GetComponent<EnemyController>().GetIsDead())
        {
            Vector3 rotation = player.position - transform.position;
            float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotationZ));

            if (!canShoot)
            {
                timeBtwShooting -= Time.deltaTime;
                if (timeBtwShooting < 0)
                {
                    canShoot = !canShoot;
                    timeBtwShooting = 1.5f;
                }
            }

            if (currentBullets <= 0)
                Invoke("Reload", 2.5f);

            if (currentBullets > 0 && canShoot)
            {
                Destroy(Instantiate(bulletPrefab, gunBarrel.position, Quaternion.identity), 3.5f);
                currentBullets--;
                canShoot = !canShoot;
            }
        }
    }

    public void Reload(){ currentBullets = capacity; }
}
