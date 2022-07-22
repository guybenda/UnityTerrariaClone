using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float MaxSpeed = 4f;
    public float Acceleration = 6f;
    public float JumpForce = 2f;

    Camera cam;
    Rigidbody2D rb;

    void Awake()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    void Update()
    {
        var newPos = transform.position;
        newPos.z = -10;
        cam.transform.position = newPos;
    }

    void FixedUpdate()
    {

    }

    void Jump()
    {
        //rb.force
    }
}
