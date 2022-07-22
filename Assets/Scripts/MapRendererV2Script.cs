using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapRendererV2Script : MonoBehaviour
{
    private Map map;

    Tilemap tilemap;

    void Awake()
    {
        map = Map.GetDefaultMap();

        tilemap = GetComponentInChildren<Tilemap>();

        tilemap.ClearAllTiles();

        /*var tiles = tilemap.GetTilesBlock(new BoundsInt(0, 0, 0, 30, 10, 1));
        Debug.Log(tiles.Length);*/

        var positions = new Vector3Int[map.Tiles.Length];
        var tiles = new TileBase[map.Tiles.Length];

        for (int w = 0; w < map.Tiles.GetLength(0); w++)
        {
            for (int h = 0; h < map.Tiles.GetLength(1); h++)
            {
                positions[map.Height * w + h] = new(w, h);
                tiles[map.Height * w + h] = map.Tiles[w, h].Tile().tile;
            }
        }

        tilemap.SetTiles(positions, tiles);
    }

    void Start()
    {
        //tilemap.
    }

    void Update()
    {

    }
}
