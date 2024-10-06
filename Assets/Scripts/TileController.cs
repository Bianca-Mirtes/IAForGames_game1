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
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && canSpawn)
        {
            System.Random random = new System.Random();

            int qntBuffers = random.Next(1, qntOfBuffers+1);
            int qntEnemies = random.Next(1, qntOfEnemies+1);

            for(int ii=0; ii < qntBuffers; ii++)
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

            for(int jj=0; jj < qntEnemies; jj++)
            {
                int posX = random.Next((int)transform.position.x - 10, (int)transform.position.x + 10); // but ik that it is fiveteen
                int posY = random.Next((int)transform.position.y - 6, (int)transform.position.y + 6); // but ik that it is ten
                spawnerPosition = new Vector3(posX, posY, 10f);

                enemies.Add(Instantiate(enemyPrefab, spawnerPosition, Quaternion.identity));
            }

            isActive = false;
            canSpawn = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isActive = false;
            canSpawn = true;
            foreach(GameObject buffer in buffers)
            {
                Destroy(buffer);
            }
        }
    }
}
