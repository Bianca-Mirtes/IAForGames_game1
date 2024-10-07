using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject playerPrefab;

    private List<GameObject> tiles;
    // Start is called before the first frame update
    void Start()
    {
        tiles = new List<GameObject>();
        StartGrid();
        StartPlayer();
    }

    public void StartGrid()
    {
        for(int ii=0; ii < width; ii++)
        {
            for(int jj=0; jj < height; jj++)
            {
                var tile = Instantiate(tilePrefab, new Vector2(ii*30, jj*20), Quaternion.identity);
                tiles.Add(tile);
                tile.name = $"Tile {ii}{jj}";
                if(ii == 0)
                {
                    foreach (BoxCollider2D bc2D in tile.transform.GetChild(0).GetComponents<BoxCollider2D>())
                    {
                        if(bc2D.offset.x < 0)
                        {
                            bc2D.enabled = true;
                        }
                        else
                        {
                            bc2D.isTrigger = true;
                            bc2D.enabled = true;
                        }
                    };
                }
                if (jj == 0)
                {
                    foreach (BoxCollider2D bc2D in tile.transform.GetChild(0).GetComponents<BoxCollider2D>())
                    {
                        if (bc2D.offset.y < 0)
                        {
                            bc2D.enabled = true;
                        }
                        else
                        {
                            bc2D.isTrigger = true;
                            bc2D.enabled = true;
                        }
                    };
                }
                if(ii == width - 1)
                {
                    foreach (BoxCollider2D bc2D in tile.transform.GetChild(0).GetComponents<BoxCollider2D>())
                    {
                        if (bc2D.offset.x > 0)
                        {
                            bc2D.enabled = true;
                        }
                        else
                        {
                            bc2D.isTrigger = true;
                            bc2D.enabled = true;
                        }
                    };
                }
                if(jj == width - 1)
                {
                    foreach (BoxCollider2D bc2D in tile.transform.GetChild(0).GetComponents<BoxCollider2D>())
                    {
                        if (bc2D.offset.y > 0)
                        {
                            bc2D.enabled = true;
                        }
                        else // it isn't exist
                        {
                            bc2D.isTrigger = true;
                            bc2D.enabled = true;
                        }
                    };
                }
                if((int)Mathf.Floor(width/2) == ii && (int)Mathf.Floor(width/2) == jj)
                {
                    foreach (BoxCollider2D bc2D in tile.transform.GetChild(0).GetComponents<BoxCollider2D>())
                    {
                        bc2D.isTrigger = true;
                        bc2D.enabled = true; // it was false 
                    };
                }
            }
        }
    }

    private void StartPlayer()
    {
        System.Random random = new System.Random();
        int tileInitialNum = random.Next(0, tiles.Count);

        Transform tileInitial = tiles[tileInitialNum].transform;
        Vector3 position = new Vector3(tileInitial.position.x, tileInitial.position.y, 10f);

        Instantiate(playerPrefab, position, Quaternion.identity);
    }
}
