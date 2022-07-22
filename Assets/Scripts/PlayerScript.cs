using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerConsts
{
    public static readonly float PLAYER_HEIGHT = 2.79f;
    public static readonly float PLAYER_WIDTH = 1.79f;

    public static readonly float MAX_SPEED_REGULAR = 4f;
    public static readonly float ACCEL_FORCE_REGULAR = 1f;

    public static readonly float JUMP_SPEED_REGULAR = 10f;
    public static readonly float JUMP_TIME_REGULAR = 0.5f;

    public static readonly float SPEED_BRAKE_REGULAR = 6f;
}

public class PlayerScript : MonoBehaviour
{
    public float MaxSpeed = PlayerConsts.MAX_SPEED_REGULAR;
    public float Acceleration = PlayerConsts.ACCEL_FORCE_REGULAR;
    public float JumpForce = PlayerConsts.JUMP_SPEED_REGULAR;
    public float JumpTime = PlayerConsts.JUMP_TIME_REGULAR;
    public float BrakeSpeed = PlayerConsts.SPEED_BRAKE_REGULAR;
    public bool Grounded = false;

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

    void UpdateCameraPosition()
    {
        var newPos = transform.position;
        newPos.z = -10;
        cam.transform.position = newPos;
    }

    void Movement()
    {
        Grounded = CheckGrounded();

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeForce(new Vector2(Acceleration, 0f), ForceMode2D.Impulse);
        }
        else
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(new Vector2(-Acceleration, 0f), ForceMode2D.Impulse);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            currentJumpTimer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Grounded)
            {
                rb.AddRelativeForce(new(0f, PlayerConsts.JUMP_SPEED_REGULAR), ForceMode2D.Impulse);
                currentJumpTimer = PlayerConsts.JUMP_TIME_REGULAR;
            }
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            if (!Grounded && currentJumpTimer>0f)
            {
                rb.AddRelativeForce(-Physics2D.gravity, ForceMode2D.Force);
            }
        }

        if (rb.velocity.x > PlayerConsts.MAX_SPEED_REGULAR)
        {
            rb.AddRelativeForce(new Vector2(-rb.velocity.x, 0).normalized * PlayerConsts.SPEED_BRAKE_REGULAR);
        }

        currentJumpTimer -= Time.fixedDeltaTime;
    }

    bool CheckGrounded()
    {
        const float BOTTOM_MARGIN = 0.1f;
        const int LAYER_MASK = ~(1 << 8);

        var point1 = new Vector2(
            transform.position.x - (PlayerConsts.PLAYER_WIDTH / 2),
            transform.position.y - (PlayerConsts.PLAYER_HEIGHT / 2) + BOTTOM_MARGIN
        );

        var point2 = new Vector2(
            transform.position.x + (PlayerConsts.PLAYER_WIDTH / 2),
            transform.position.y - (PlayerConsts.PLAYER_HEIGHT / 2) - BOTTOM_MARGIN
        );

        Debug.DrawRay(point2, point1 - point2, Color.red, 0.1f);
        var hit = Physics2D.OverlapArea(point1, point2, LAYER_MASK);

        return hit != null;
    }
}
