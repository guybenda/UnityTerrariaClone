using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerConsts
{
    public static readonly float PLAYER_HEIGHT = 2.79f;
    public static readonly float PLAYER_WIDTH = 1.79f;

    public static readonly float MAX_SPEED_REGULAR = 11.25f;
    public static readonly float ACCELERATION_REGULAR = 0.3f;

    public static readonly float BRAKE_REGULAR = 0.75f;

    public static readonly float AIR_CONTROLL_MULT = 0.5f;

    public static readonly float JUMP_SPEED_REGULAR = 17f;
    public static readonly float JUMP_TIME_REGULAR = 0.267f;
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

        HorizontalMovement();
        Movement();
        ClampVerticalVelocity();

        jump = false;
    }

    void UpdateCameraPosition()
    {
        var newPos = transform.position;
        newPos.z = -10;
        cam.transform.position = newPos;
    }

    void HorizontalMovement()
    {
        var currentVelocity = rb.velocity.x;

        var newVelocity = rb.velocity.x;

        var verticalVelocity = rb.velocity.y;
        bool standingStill = rb.velocity.x < 0.001;

        float forceToApply = (Mathf.Abs(currentVelocity) > Mathf.Abs(targetMovement)) ? 


        /*
        bool oppositeMagnitude = Mathf.Sign(currentVelocity) != Mathf.Sign(targetMovement);
        var forceToBeAppllied = targetMovement;

        if (!Grounded) forceToBeAppllied *= PlayerConsts.AIR_CONTROLL_MULT;

        if (!oppositeMagnitude || standingStill)
        {
            if (Mathf.Abs(currentVelocity) > PlayerConsts.MAX_SPEED_REGULAR)
            {
                newVelocity -= Mathf.Sign(currentVelocity) * Mathf.Clamp(currentVelocity, 0, PlayerConsts.BRAKE_REGULAR);
            }
            else
            {
                newVelocity = Mathf.Clamp(currentVelocity += forceToBeAppllied,-PlayerConsts.MAX_SPEED_REGULAR, PlayerConsts.MAX_SPEED_REGULAR);
                //rb.AddRelativeForce(new Vector2(forceToBeAppllied, 0f), ForceMode2D.Impulse);
            }

        }
        else if (targetMovement == 0f || oppositeMagnitude)
        {
            newVelocity -= Mathf.Sign(currentVelocity) * Mathf.Clamp(currentVelocity, 0, PlayerConsts.BRAKE_REGULAR);
            //rb.velocity = new(Mathf.Clamp())
        }*/

        if (Mathf.Abs(newVelocity) < 0.001)
        {
            newVelocity = 0f;
        }

        /*
         * 
            rb.AddRelativeForce(new Vector2(10f, 0f), ForceMode2D.Impulse);


        if (targetMovement > 0 && currentVelocity > -PlayerConsts.MAX_SPEED_REGULAR)
        {
            if (Mathf.Abs(rb.velocity.y) < 0.01f)
            {
                if (currentVelocity > PlayerConsts.SLOWDOWN_REGULAR)
                {
                    newVelocity -= PlayerConsts.SLOWDOWN_REGULAR;
                }

                newVelocity -= PlayerConsts.ACCELERATION_REGULAR;
            }
        }
        else if (targetMovement < 0 && currentVelocity < PlayerConsts.MAX_SPEED_REGULAR)
        {
            if (Mathf.Abs(rb.velocity.y) < 0.01f)
            {
                if (currentVelocity < -PlayerConsts.SLOWDOWN_REGULAR)
                {
                    newVelocity += PlayerConsts.SLOWDOWN_REGULAR;
                }

                newVelocity += PlayerConsts.ACCELERATION_REGULAR;
            }
        }
        else
        {
            if (Mathf.Abs(rb.velocity.y) < 0.01f)
            {
                if (currentVelocity > PlayerConsts.SLOWDOWN_REGULAR * 0.5f)
                {
                    newVelocity -= PlayerConsts.SLOWDOWN_REGULAR * 0.5f;
                }
                else if (currentVelocity < (double)(0f - PlayerConsts.SLOWDOWN_REGULAR) * 0.5)
                {
                    newVelocity += PlayerConsts.SLOWDOWN_REGULAR * 0.5f;
                }
                else
                {
                    newVelocity = 0f;
                }
            }
            else
            {
                if (currentVelocity > PlayerConsts.SLOWDOWN_REGULAR)
                {
                    newVelocity -= PlayerConsts.SLOWDOWN_REGULAR;
                }
                else if (currentVelocity < -PlayerConsts.SLOWDOWN_REGULAR)
                {
                    newVelocity += PlayerConsts.SLOWDOWN_REGULAR;
                }
                else
                {
                    newVelocity = 0f;
                }
            }
        }*/

        rb.velocity = new(newVelocity, rb.velocity.y);
    }

    void Movement()
    {
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
        const float BOTTOM_MARGIN = 0.05f;
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

    void ClampVerticalVelocity()
    {
        rb.velocity = new(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -36.5f, float.MaxValue));
    }
}
