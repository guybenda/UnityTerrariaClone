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

        for (int w = 0; w < Map.Tiles.GetLength(0); w++)
        {
            for (int h = 0; h < Map.Tiles.GetLength(1); h++)
            {
                Tilemap.SetTile(new Vector3Int(w, h), Map.Tiles[w, h].Tile().tile);
            }
        }

        stopWatch.Stop();
        TimeSpan ts = stopWatch.Elapsed;

        UnityEngine.Debug.Log("Initial render took " + ts.TotalMilliseconds + "ms");
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
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        var counter = 0;

        foreach (var position in Map.Tiles.changes)
        {
            Tilemap.SetTile(position.Vector(), Map.Tiles[position.Item1, position.Item2].Tile().tile);
            counter++;
        }

        Map.Tiles.changes.Clear();

        stopWatch.Stop();
        TimeSpan ts = stopWatch.Elapsed;

        if (counter > 0)
            UnityEngine.Debug.Log("Rendered " + counter + " tiles in " + ts.TotalMilliseconds + "ms");
    }
}
