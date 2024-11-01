using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private Camera mainC;
    private Vector3 mousePos;
    private bool canShoot;
    private Transform playerUI;
    private int currentBullets;

    [SerializeField] private float timeBtwShooting = 0.7f;
    [SerializeField] private int capacity = 20;

    public GameObject bulletPrefab;
    public Transform gunBarrel;
    // Start is called before the first frame update
    void Start()
    {
        mainC = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerUI = transform.parent.GetChild(2);
        Int32.TryParse(playerUI.parent.GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>().text, out currentBullets);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainC.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;

        float rotationZ = Mathf.Atan2(rotation.y, rotation.x)*Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotationZ));

        if (!canShoot)
        {
            timeBtwShooting -= Time.deltaTime;
            if(timeBtwShooting < 0)
            {
                canShoot = !canShoot;
                timeBtwShooting = 0.7f;
            }
        }
        if(Input.GetMouseButton(0) && canShoot && currentBullets > 0)
        {
            Destroy(Instantiate(bulletPrefab, gunBarrel.position, Quaternion.identity), 3.5f);
            currentBullets--;
            playerUI.GetChild(3).GetComponent<TextMeshProUGUI>().text = currentBullets + "";
            canShoot = !canShoot;
        }
    }

    public int GetCapacity() { return capacity; }

    public int GetCurrentBullets() { return currentBullets; }

    public void SetCurrentBullets(int value)
    {
        if (currentBullets == capacity)
            return;

        if(currentBullets+value > capacity)
            currentBullets += capacity - currentBullets;
        else
            currentBullets += value;

        playerUI.GetChild(3).GetComponent<TextMeshProUGUI>().text = currentBullets + "";
    }
}
