using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Tile Tile
    {
        get { return Tile; }
        set { UpdateMaterial(value); }
    }
    public Vector2Int Position
    {
        get { return Position; }
        set { UpdatePosition(value); }
    }

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (Tile != null)
        {
            spriteRenderer.material = Tile.material;
        }

        transform.position = (Vector2)Position;
    }

    void Start()
    {

    }

    void UpdateMaterial(Tile tile)
    {
        Tile = tile;
        spriteRenderer.material = tile.material;
    }

    void UpdatePosition(Vector2Int pos)
    {
        Position = pos;
        transform.position = (Vector2)pos;
    }
}
