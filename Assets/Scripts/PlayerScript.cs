using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Vector2 _position;
    public Vector2 Position
    {
        get { return _position; }
        set {
            _position = value;
            transform.position = value;
        }
    }

    private Vector2 _velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
