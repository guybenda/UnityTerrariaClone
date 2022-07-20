using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    protected Map map;

    void Awake()
    {
        map = new Map(20, 10);

        InitializeTiles();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    void InitializeTiles()
    {
        for (int i = 0; i < map.Tiles.GetLength(0); i++)
        {
            map.Tiles[i, 0] = TileId.Stone;
            map.Tiles[i, 1] = TileId.Dirt;
        }
    }
}
