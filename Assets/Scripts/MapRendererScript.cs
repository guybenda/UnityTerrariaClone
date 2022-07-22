using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class MapRendererScript : MonoBehaviour
{
    private Map map;

    TileScript[,] tiles;

    void Awake()
    {
        map = Map.GetDefaultMap();

        tiles = new TileScript[map.Width, map.Height];

        for (int w = 0; w < map.Tiles.GetLength(0); w++)
        {
            for (int h = 0; h < map.Tiles.GetLength(1); h++)
            {
                tiles[w,h] = TileScript.CreateTile(map.Tiles[w,h].Tile(), new Vector2Int(w,h)).GetComponent<TileScript>();

                tiles[w, h].transform.parent = gameObject.transform;
            }
        }
    }

    void Start()
    {

        for (int i = 0; i < tiles.Length; i++)
        {

        }
    }

    void Update()
    {
        
    }
}*/
