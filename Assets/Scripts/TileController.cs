using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public GameObject ammonBoxPrefab;
    public GameObject healthBoxPrefab;
    public GameObject enemyPrefab;

    private bool isActive = false;
    private bool canSpawn = true;
    private bool playerIsHere = false;
    private Vector3 spawnerPosition;

    private List<GameObject> buffers;
    private List<GameObject> enemies;

    [Range(1, 4)]
    [SerializeField] int qntOfBuffers;

    [Range(1, 5)]
    [SerializeField] int qntOfEnemies;

    private void Start()
    {
        buffers = new List<GameObject>();
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if(canSpawn)
            {
                System.Random random = new System.Random();

                int qntBuffers = random.Next(1, qntOfBuffers + 1);
                int qntEnemies = random.Next(1, qntOfEnemies + 1);

                for (int ii = 0; ii < qntBuffers; ii++)
                {
                    int type = random.Next(1, 101);

                    int posX = random.Next((int)transform.position.x - 10, (int)transform.position.x + 10); // but ik that it is fiveteen
                    int posY = random.Next((int)transform.position.y - 6, (int)transform.position.y + 6); // but ik that it is ten
                    spawnerPosition = new Vector3(posX, posY, 10f);
                    if (type % 2 == 0)
                    {
                        buffers.Add(Instantiate(ammonBoxPrefab, spawnerPosition, Quaternion.identity));
                    }
                    else
                    {
                        buffers.Add(Instantiate(healthBoxPrefab, spawnerPosition, Quaternion.identity));
                    }
                }
                for(int jj=0; jj < qntEnemies; jj++) {
                    int posX = random.Next((int)transform.position.x - 10, (int)transform.position.x + 10); // but ik that it is fiveteen
                    int posY = random.Next((int)transform.position.y - 6, (int)transform.position.y + 6); // but ik that it is ten
                    spawnerPosition = new Vector3(posX, posY, 10f);

                    enemies.Add(Instantiate(enemyPrefab, spawnerPosition, Quaternion.identity));
                }
                canSpawn = false;
            }
            else
            {
                foreach(var buffer in buffers)
                {
                    buffer.SetActive(true);
                }

                foreach (var enemy in enemies)
                {
                    enemy.SetActive(true);
                }
            }
            isActive = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isActive = true;
            playerIsHere = true;
        }
    }

    public bool PlayerIsHere()
    {
        return playerIsHere;
    }

    public void SetPlayerIsHere(bool value)
    {
        playerIsHere = value;
    }

    public void SetAreaActivation(bool value)
    {
        isActive = value;
        if (!isActive)
        {
            foreach (var buffer in buffers)
            {
                buffer.SetActive(false);
            }
            foreach (var enemy in enemies)
            {
                enemy.SetActive(false);
            }
        }
    }

    public bool GetIsActive()
    {
        return isActive;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isActive = false;
            playerIsHere = false;
        }
    }
}
