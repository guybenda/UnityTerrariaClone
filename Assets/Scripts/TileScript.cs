using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Tile Tile
    {
        get { return Tile; }
        set
        {
            Tile = value;
            spriteRenderer.material = value?.material;
        }
    }
    public Vector2Int Position
    {
        get { return Position; }
        set
        {
            Position = value;
            transform.localPosition = (Vector2)value;
        }
    }

    public static GameObject prefab { get; private set; }

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        if (prefab == null)
        {
            prefab = Resources.Load<GameObject>("Prefabs/Tile");
        }

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (Tile != null)
        {
            spriteRenderer.material = Tile.material;
        }

        transform.localPosition = (Vector2)Position;
    }

    void Start()
    {

    }

    public GameObject CreateTile()
    {
        var tile = Instantiate(prefab);

        return null;
    }
}
