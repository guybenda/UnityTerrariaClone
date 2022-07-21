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
            spriteRenderer.sprite = value.sprite.Sprite().sprite;
            spriteRenderer.color = value.sprite.Sprite().color;
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

    private static GameObject prefab { get; set; }

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {

    }

    public static GameObject CreateTile()
    {
        if (prefab == null)
        {
            prefab = Resources.Load<GameObject>("Prefabs/Tile");
            Debug.Log(prefab);
        }

        var tile = Instantiate(prefab);
        // TODO
        return tile;
    }
}
