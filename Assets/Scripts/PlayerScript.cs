using Assets.Scripts.Game;
using UnityEngine;

public struct PlayerConsts
{
    public static readonly float MAX_SPEED_REGULAR = 11.25f;
    public static readonly float ACCELERATION_REGULAR = 0.3f;
    public static readonly float BRAKE_REGULAR = 0.75f;

    public static readonly float AIR_CONTROLL_MULT = 0.75f;

    public static readonly float JUMP_SPEED_REGULAR = 17f;
    public static readonly float JUMP_TIME_REGULAR = 0.28f;

    public static readonly float RANGE = 5f;

}

public class PlayerScript : MonoBehaviour
{
    private bool Grounded = false;
    private bool jump = false;
    private float currentJumpTimer = 0f;
    private float targetMovement = 0f;
    private bool lastPressedA = false;

    private Vector2 centerOfScreen;
    private float height;
    private float width;
    private Vector2 hitboxOffset;


    Camera cam;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    MapRendererScript map;

    void Awake()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<MapRendererScript>();
        boxCollider = GetComponent<BoxCollider2D>();

        width = boxCollider.size.x;
        height = boxCollider.size.y;
        hitboxOffset = boxCollider.offset;
    }

    void Start()
    {

    }

    void Update()
    {
        UpdateCameraPosition();

        HandleKeyboardInput();

        HandleMouseInput();
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
        var newPos = transform.position - (Vector3)hitboxOffset;
        newPos.z = -10;
        cam.transform.position = newPos;
    }

    void HorizontalMovement()
    {
        var currentVelocity = rb.velocity.x;

        var newVelocity = rb.velocity.x;

        float brakeForce = -Mathf.Sign(currentVelocity) * PlayerConsts.BRAKE_REGULAR;

        float accelForce = Mathf.Sign(targetMovement) * PlayerConsts.ACCELERATION_REGULAR;
        /*if (Mathf.Abs(currentVelocity) + Mathf.Abs(accelForce) > PlayerConsts.MAX_SPEED_REGULAR)
        {
            Debug.Log("MAX SPEED!!!!!!!!!!!!! " + currentVelocity);

            accelForce -= (PlayerConsts.MAX_SPEED_REGULAR - (Mathf.Abs(currentVelocity) + Mathf.Abs(accelForce))) * Mathf.Sign(currentVelocity);
        }*/

        if (!Grounded)
        {
            accelForce *= PlayerConsts.AIR_CONTROLL_MULT;
            brakeForce *= PlayerConsts.AIR_CONTROLL_MULT;
        }


        if (Mathf.Abs(currentVelocity) > PlayerConsts.MAX_SPEED_REGULAR + 0.25f)
        {
            Debug.Log("MAX SPEED! " + currentVelocity);
            newVelocity = Mathf.Max(newVelocity, newVelocity * 0.9f);
        }

        if (targetMovement == 0f)
        {
            newVelocity += brakeForce * 0.8f;

            if (Mathf.Sign(newVelocity) != Mathf.Sign(currentVelocity))
            {
                newVelocity = 0f;
            }
        }
        else
        {
            if (Mathf.Sign(currentVelocity) != Mathf.Sign(targetMovement))
            {
                newVelocity += brakeForce;
            }
            else if (Mathf.Abs(currentVelocity) > Mathf.Abs(newVelocity))
            {
                newVelocity += brakeForce;
            }
            else
            {
                newVelocity += accelForce;
                if (Mathf.Abs(newVelocity) > PlayerConsts.MAX_SPEED_REGULAR) newVelocity = Mathf.Clamp(newVelocity, -PlayerConsts.MAX_SPEED_REGULAR, PlayerConsts.MAX_SPEED_REGULAR);
            }
        }

        if (Mathf.Abs(newVelocity) < 0.001)
        {
            newVelocity = 0f;
        }

        rb.velocity = new(newVelocity, rb.velocity.y);
    }

    void Movement()
    {
        // Jump
        if (Grounded && jump)
        {
            var newVelocity = rb.velocity;
            newVelocity.y = PlayerConsts.JUMP_SPEED_REGULAR;
            rb.velocity = newVelocity;
            currentJumpTimer = PlayerConsts.JUMP_TIME_REGULAR;
        }
        else if ((!Grounded && !Input.GetKey(KeyCode.Space) && currentJumpTimer > 0f) || rb.velocity.y <= 0.01f)
        {
            currentJumpTimer = 0f;

            //Debug.Log("Stopped jump!!");
        }
        else if (!Grounded && Input.GetKey(KeyCode.Space) && currentJumpTimer > 0f)
        {
            rb.AddRelativeForce(-Physics2D.gravity, ForceMode2D.Force);
        }
        else
        {
            rb.AddRelativeForce(new(0f, -0.2f), ForceMode2D.Impulse);
        }

        currentJumpTimer -= Time.fixedDeltaTime;

    }

    bool CheckGrounded()
    {
        const float BOTTOM_MARGIN = 0.02f;
        const int LAYER_MASK = ~(1 << 8);

        var point1 = new Vector2(
            transform.position.x - (width / 2) + BOTTOM_MARGIN,
            transform.position.y - (height / 2) + BOTTOM_MARGIN + hitboxOffset.y
        );

        var point2 = new Vector2(
            transform.position.x + (width / 2) - BOTTOM_MARGIN,
            transform.position.y - (height / 2) - BOTTOM_MARGIN + hitboxOffset.y
        );

        var hit = Physics2D.OverlapArea(point1, point2, LAYER_MASK);

        return hit != null;
    }

    void HandleKeyboardInput()
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

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

            var pos = map.Tilemap.WorldToCell(mousePosition);

            if (Vector2.Distance(transform.position, mousePosition) > PlayerConsts.RANGE)
            {

            }
            else
            {

            }


            map.Map.Tiles[pos.x, pos.y] = TileId.Dirt;
        }
    }

    void ClampVerticalVelocity()
    {
        rb.velocity = new(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -36.5f, float.MaxValue));
    }

    private void MoveClamped(Vector2 direction)
    {

    }
}
