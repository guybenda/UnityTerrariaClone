using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerConsts
{
    public static readonly float MAX_SPEED_REGULAR = 4f;
    public static readonly float MAX_ACCEL_REGULAR = 6f;
    
    public static readonly float JUMP_SPEED_REGULAR = 10f;
    public static readonly float JUMP_TIME_REGULAR = 0.5f;
}

public class PlayerScript : MonoBehaviour
{
    public float MaxSpeed = PlayerConsts.MAX_SPEED_REGULAR;
    public float Acceleration = PlayerConsts.MAX_ACCEL_REGULAR;
    public float JumpForce = PlayerConsts.JUMP_SPEED_REGULAR;
    public float JumpTime = PlayerConsts.JUMP_TIME_REGULAR;

    private float currentJumpTimer = 0f;

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
        UpdateCameraPosition();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Jump()
    {
        //rb.force
    }

    void UpdateCameraPosition()
    {
        var newPos = transform.position;
        newPos.z = -10;
        cam.transform.position = newPos;
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("D");
            rb.AddRelativeForce(new Vector2(Acceleration, 0f));
        }
        else
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("A");
            rb.AddRelativeForce(new Vector2(-Acceleration, 0f));
        }
    }
}
