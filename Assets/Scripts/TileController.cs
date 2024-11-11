using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class TileController : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject ammonBoxPrefab;
    public GameObject healthBoxPrefab;
    public GameObject enemyPrefab;
    public GameObject floorPrefab;
    private List<GameObject> buffers;
    private List<GameObject> enemies;

    [Header("Booleanos")]
    private bool isActive = false;
    private bool canSpawn = true;

    private List<GameObject> floors;

    public int sizeX = 5;
    public int sizeY = 5;
    private int width;
    private int height;

    private Vector3 spawnerPosition;

    [Range(1, 4)]
    [SerializeField] int qntOfBuffers;

    [Range(1, 5)]
    [SerializeField] int qntOfEnemies;

    private void Start()
    {
        buffers = new List<GameObject>();
        enemies = new List<GameObject>();
        width = FindObjectOfType<GridManager>().getWidth();
        height = FindObjectOfType<GridManager>().getHeight();
        floors = new List<GameObject>();

        for(int x = (int)transform.position.x - sizeX; x <= (int)transform.position.x + sizeX; x+=(int)floorPrefab.transform.localScale.x -1) {
            for(float y = (int)transform.position.y - sizeY; y <= (int)transform.position.y + sizeY; y+=(int)floorPrefab.transform.localScale.y -1) {
                Vector3 position = new Vector3(x, y, 2f);
                floors.Add(Instantiate(floorPrefab, position, Quaternion.identity));
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
                foreach(GameObject buffer in buffers)
                {
                    buffer.SetActive(true);
                }

                foreach(GameObject enemy in enemies)
                {
                    enemy.SetActive(true);
                }
                
                ModifyStateBufferFloor(true);
            }
        }
        else
        {
            foreach (GameObject buffer in buffers)
            {
                buffer.SetActive(false);
            }

            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(false);
            }


            ModifyStateBufferFloor(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            isActive = true;
    }

    public void setIsActiveArea(bool value)
    {
        isActive = value;
    }

    public void SetNeighborsArea(bool value)
    {
        string[] split = transform.name.Split(' ');
        int x = int.Parse(split[1]);
        int y = int.Parse(split[2]);
        // verifica as quatro direções possiveis
        // se existir, ativa
        GameObject tile1 = GameObject.Find("Area " + (x + 1) + " " + y);
        if (tile1 != null) { tile1.GetComponent<TileController>().setIsActiveArea(value); }

        GameObject tile2 = GameObject.Find("Area " + (x - 1) + " " + y);
        if (tile2 != null) { tile2.GetComponent<TileController>().setIsActiveArea(value); }

        GameObject tile3 = GameObject.Find("Area " + x + " " + (y + 1));
        if (tile3 != null) { tile3.GetComponent<TileController>().setIsActiveArea(value); }

        GameObject tile4 = GameObject.Find("Area " + x + " " + (y - 1));
        if (tile4 != null) { tile4.GetComponent<TileController>().setIsActiveArea(value); }
    }

    private void ModifyStateBufferFloor(bool state) {
        foreach (var f in floors)
            f.SetActive(state);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    public void RemoveBuffer(GameObject buffer)
    {
        buffers.Remove(buffer);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isActive = false;
            SetNeighborsArea(false);
        }
    }
}
