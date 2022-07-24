using Assets.Scripts.Game;
using Assets.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapRendererV2Script : MonoBehaviour
{
    public Map Map { get; private set; }

    public Tilemap Tilemap { get; private set; }

    void Awake()
    {
        Tilemap = GetComponentInChildren<Tilemap>();
    }

    void Start()
    {
        Map = Map.GetDefaultMap();

        Tilemap.ClearAllTiles();

        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        //var positions = new Vector3Int[map.Tiles.Length];
        //var tiles = new TileBase[map.Tiles.Length];

        for (int w = 0; w < Map.Tiles.GetLength(0); w++)
        {
            for (int h = 0; h < Map.Tiles.GetLength(1); h++)
            {
                //positions[map.Height * w + h] = new(w, h);
                //tiles[map.Height * w + h] = map.Tiles[w, h].Tile().tile;
                Tilemap.SetTile(new Vector3Int(w, h), Map.Tiles[w, h].Tile().tile);
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

    void FixedUpdate()
    {
        RenderChanges();
    }

    void RenderChanges()
    {
        foreach (var position in Map.Tiles.changes)
        {
            Tilemap.SetTile(position.Vector(), Map.Tiles[position.Item1, position.Item2].Tile().tile);
        }
    }
}
