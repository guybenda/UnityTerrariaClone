using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    private Tile _tile;
    public Tile Tile
    {
        get { return _tile; }
        set
        {
            _tile = value;
            spriteRenderer.sprite = value?.sprite.Sprite().sprite;
            spriteRenderer.color = value?.sprite.Sprite().color ?? Color.black;
            boxCollider.enabled = value?.solid ?? false;
        }
    }

    private Vector2Int _position;
    public Vector2Int Position
    {
        get { return _position; }
        set
        {
            _position = value;
            transform.localPosition = (Vector2)value;
        }
    }

    private float _scale;
    public float Scale
    {
        get { return _scale; }
        set
        {
            _scale = value;
            transform.localScale = new Vector3(value, value, value);
        }
    }

    private static GameObject prefab;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {

    }

    public static GameObject CreateTile(Tile tile = null, Vector2Int position = new Vector2Int())
    {
        if (prefab == null)
        {
            prefab = Resources.Load<GameObject>("Prefabs/Tile");
        }

        var tileObject = Instantiate(prefab);

        var tileScript = tileObject.GetComponent<TileScript>();
        tileScript.Position = position;
        tileScript.Tile = tile;

        return tileObject;
    }
}
