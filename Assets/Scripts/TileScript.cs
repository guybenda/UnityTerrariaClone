using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript:MonoBehaviour
{
    public Tile Tile { get { return Tile; } set { } }

    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    public void UpdateProperties(Tile tile)
    {
        this.Tile = tile;

        // Update data
    }
}
