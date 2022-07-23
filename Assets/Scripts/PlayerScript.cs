using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerConsts
{
    public static readonly float PLAYER_HEIGHT = 2.79f;
    public static readonly float PLAYER_WIDTH = 1.79f;

    public static readonly float MAX_SPEED_REGULAR = 11f;
    public static readonly float ACCEL_FORCE_REGULAR = 1f;

    public static readonly float JUMP_SPEED_REGULAR = 17.2875f;
    public static readonly float JUMP_TIME_REGULAR = 0.267f;

    public static readonly float SPEED_ACCEL_LERP = 0.07f;
    public static readonly float SPEED_BRAKE_LERP = 0.3f;
}

public class PlayerScript : MonoBehaviour
{
    private bool Grounded = false;

    private bool jump = false;
    private float currentJumpTimer = 0f;

    private float targetMovement = 0f;

    private bool lastPressedA = false;

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

        HandleInput();
    }

    void FixedUpdate()
    {
        Grounded = CheckGrounded();

        Movement();
        ClampVelocity();

        jump = false;
    }

    void UpdateCameraPosition()
    {
        var newPos = transform.position;
        newPos.z = -10;
        cam.transform.position = newPos;
    }

    void Movement()
    {
        // Movement
        var horizontalVelocity = rb.velocity.x;

        if (targetMovement == 0f)
        {
            if (rb.velocity.x > 0)
            {
                rb.velocity = new(Mathf.Clamp(rb.velocity.x, 0f, rb.velocity.x - PlayerConsts.SPEED_BRAKE_LERP), rb.velocity.y);
            }
            else
            {
                rb.velocity = new(Mathf.Clamp(rb.velocity.x, rb.velocity.x + PlayerConsts.SPEED_BRAKE_LERP, 0f), rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = new(Mathf.Lerp(horizontalVelocity, targetMovement, PlayerConsts.SPEED_ACCEL_LERP), rb.velocity.y);
        }

        // Jump
        if (Grounded && jump)
        {
            //rb.AddRelativeForce(new(0f, PlayerConsts.JUMP_SPEED_REGULAR), ForceMode2D.Impulse);
            var newVelocity = rb.velocity;
            newVelocity.y = PlayerConsts.JUMP_SPEED_REGULAR;
            rb.velocity = newVelocity;
            currentJumpTimer = PlayerConsts.JUMP_TIME_REGULAR;
        }
        else if ((!Grounded && !Input.GetKey(KeyCode.Space) && currentJumpTimer > 0f) || rb.velocity.y <= 0.1f)
        {
            currentJumpTimer = 0f;
        }
        else if (!Grounded && Input.GetKey(KeyCode.Space) && currentJumpTimer > 0f)
        {
            rb.AddRelativeForce(-Physics2D.gravity, ForceMode2D.Force);
        }

        currentJumpTimer -= Time.fixedDeltaTime;


        if (rb.velocity.x > PlayerConsts.MAX_SPEED_REGULAR)
        {
            //rb.AddRelativeForce(new Vector2(-rb.velocity.x, 0).normalized * PlayerConsts.SPEED_BRAKE_REGULAR);
        }

    }

    bool CheckGrounded()
    {
        const float BOTTOM_MARGIN = 0.1f;
        const int LAYER_MASK = ~(1 << 8);

        var point1 = new Vector2(
            transform.position.x - (PlayerConsts.PLAYER_WIDTH / 2) + BOTTOM_MARGIN,
            transform.position.y - (PlayerConsts.PLAYER_HEIGHT / 2) + BOTTOM_MARGIN
        );

        var point2 = new Vector2(
            transform.position.x + (PlayerConsts.PLAYER_WIDTH / 2) - BOTTOM_MARGIN,
            transform.position.y - (PlayerConsts.PLAYER_HEIGHT / 2) - BOTTOM_MARGIN
        );

        Debug.DrawRay(point2, point1 - point2, Color.red, 0.1f);
        var hit = Physics2D.OverlapArea(point1, point2, LAYER_MASK);

        return hit != null;
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            lastPressedA = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            lastPressedA = false;
        }

        // TODO make this less shit
        if (lastPressedA)
        {

            if (Input.GetKey(KeyCode.A))
            {
                targetMovement = -PlayerConsts.MAX_SPEED_REGULAR;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                targetMovement = PlayerConsts.MAX_SPEED_REGULAR;
            }
            else
            {
                targetMovement = 0f;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                targetMovement = PlayerConsts.MAX_SPEED_REGULAR;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                targetMovement = -PlayerConsts.MAX_SPEED_REGULAR;
            }
            else
            {
                targetMovement = 0f;
            }
        }
    }

    void ClampVelocity()
    {
        rb.velocity = new(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -36.5f, float.MaxValue));
    }
}
