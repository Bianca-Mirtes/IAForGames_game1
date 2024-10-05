using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private GameObject tilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartGrid();
    }

    public void StartGrid()
    {
        for(int ii=0; ii < width; ii++)
        {
            for(int jj=0; jj < height; jj++)
            {
                var tile = Instantiate(tilePrefab, new Vector2(ii*30, jj*20), Quaternion.identity);
                tile.name = $"Tile {ii}{jj}";
                if(ii == 0)
                {
                    foreach (BoxCollider2D bc2D in tile.GetComponents<BoxCollider2D>())
                    {
                        if(bc2D.offset.x < 0)
                        {
                            bc2D.enabled = true;
                        }
                    };
                }
                if (jj == 0)
                {
                    foreach (BoxCollider2D bc2D in tile.GetComponents<BoxCollider2D>())
                    {
                        if (bc2D.offset.y < 0)
                        {
                            bc2D.enabled = true;
                        }
                    };
                }
                if(ii == width - 1)
                {
                    foreach (BoxCollider2D bc2D in tile.GetComponents<BoxCollider2D>())
                    {
                        if (bc2D.offset.x > 0)
                        {
                            bc2D.enabled = true;
                        }
                    };
                }
                if(jj == width - 1)
                {
                    foreach (BoxCollider2D bc2D in tile.GetComponents<BoxCollider2D>())
                    {
                        if (bc2D.offset.y > 0)
                        {
                            bc2D.enabled = true;
                        }
                    };
                }
                if(ii == jj)
                {
                    if((width/2)-0.5f == ii && (width / 2) - 0.5f == jj)
                    {
                        foreach (BoxCollider2D bc2D in tile.GetComponents<BoxCollider2D>())
                        {
                            bc2D.enabled = false;
                        };
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
