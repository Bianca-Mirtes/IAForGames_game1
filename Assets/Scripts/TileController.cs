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
    public GameObject floor;

    private List<GameObject> floors;

    public int sizeX = 5;

    public int sizeY = 5;
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
        floors = new List<GameObject>();

        for(int x = (int)transform.position.x - sizeX; x <= (int)transform.position.x + sizeX; x+=(int)floor.transform.localScale.x -1) {
            for(float y = (int)transform.position.y - sizeY; y <= (int)transform.position.y + sizeY; y+=(int)floor.transform.localScale.y -1) {
                Vector3 position = new Vector3(x, y, 2f);
                floors.Add(Instantiate(floor, position, Quaternion.identity));
            }
        }
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

                    int posX = random.Next((int)transform.position.x - sizeX, (int)transform.position.x + sizeX); // but ik that it is fiveteen
                    int posY = random.Next((int)transform.position.y - sizeY, (int)transform.position.y + sizeY); // but ik that it is ten
                    spawnerPosition = new Vector3(posX, posY, 10f);
                    if (type % 2 == 0)
                        buffers.Add(Instantiate(ammonBoxPrefab, spawnerPosition, Quaternion.identity));
                    else
                        buffers.Add(Instantiate(healthBoxPrefab, spawnerPosition, Quaternion.identity));
                }
                for(int jj=0; jj < qntEnemies; jj++) {
                    int posX = random.Next((int)transform.position.x - sizeX, (int)transform.position.x + sizeX); // but ik that it is fiveteen
                    int posY = random.Next((int)transform.position.y - sizeY, (int)transform.position.y + sizeY); // but ik that it is ten
                    spawnerPosition = new Vector3(posX, posY, 10f);

                    enemies.Add(Instantiate(enemyPrefab, spawnerPosition, Quaternion.identity));
                }

                canSpawn = false;
            }
            else
            {
                foreach(var buffer in buffers)
                    buffer.SetActive(true);

                foreach (var enemy in enemies)
                    enemy.SetActive(true);

                ModifyStateBufferFloor(true);
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

    public bool PlayerIsHere() { return playerIsHere;  }

    public void SetPlayerIsHere(bool value) { playerIsHere = value; }

    public void SetAreaActivation(bool value)
    {
        isActive = value;
        if (!isActive)
        {
            foreach (var buffer in buffers)
                buffer.SetActive(false);

            foreach (var enemy in enemies)
                enemy.SetActive(false);

            ModifyStateBufferFloor(false);
        }
    }

    private void ModifyStateBufferFloor(bool state) {
        foreach (var f in floors)
            f.SetActive(state);
    }

    public bool GetIsActive() { return isActive; }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isActive = false;
            playerIsHere = false;
        }
    }
}
