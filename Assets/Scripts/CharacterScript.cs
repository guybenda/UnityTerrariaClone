using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private Vector2 _position;
    public Vector2 Position
    {
        get { return _position; }
        set
        {
            _position = value;
            transform.position = value;
        }
    }

    private Vector2 _velocity;


    void Start()
    {

    }

    void Update()
    {

    }
}
