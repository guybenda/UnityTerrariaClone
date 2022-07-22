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

        var tiles = tilemap.GetTilesBlock(new BoundsInt(0, 0, 0, 30, 10, 1));
        Debug.Log(tiles.Length);
    }

    void Start()
    {
        //tilemap.
    }

    void Update()
    {

    }
}
