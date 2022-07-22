using Assets.Scripts.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapRendererV2Script : MonoBehaviour
{
    private Map map;

    Tilemap tilemap;

    void Awake()
    {

        tilemap = GetComponentInChildren<Tilemap>();

    }

    void Start()
    {
        map = Map.GetDefaultMap();

        tilemap.ClearAllTiles();

        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        //var positions = new Vector3Int[map.Tiles.Length];
        //var tiles = new TileBase[map.Tiles.Length];

        for (int w = 0; w < map.Tiles.GetLength(0); w++)
        {
            for (int h = 0; h < map.Tiles.GetLength(1); h++)
            {
                //positions[map.Height * w + h] = new(w, h);
                //tiles[map.Height * w + h] = map.Tiles[w, h].Tile().tile;
                tilemap.SetTile(new Vector3Int(w, h), map.Tiles[w, h].Tile().tile);
            }
        }

        //tilemap.SetTiles(positions, tiles);

        stopWatch.Stop();
        // Get the elapsed time as a TimeSpan value.
        TimeSpan ts = stopWatch.Elapsed;

        UnityEngine.Debug.Log(ts.TotalMilliseconds + "ms");
    }

    void Update()
    {

    }
}
