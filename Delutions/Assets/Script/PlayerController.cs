using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed = 10f, speedUp = 75f;
    float currentSpeed;

    bool isGrounded;
    bool jump;
    bool holding;
    float jumpBuffer;
    public Transform groundCheck;
    public float radius = 0.2f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;

    [HideInInspector]
    public bool clamp = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, groundMask);
        Jump();

        if (jump) jumpBuffer -= Time.deltaTime;
        if (jumpBuffer <= 0) jump = false;
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");

        Move(x);

        if (clamp) ClampVel(x);
    }
    void Move(float x)
    {
        rb.AddForce(Vector2.right * x * speedUp);

    }
    void ClampVel(float x)
    {
        Vector2 velocity = new Vector2(rb.velocity.x, 0);
        Vector2 vel = Vector2.ClampMagnitude(velocity, speed);
        rb.velocity = new Vector2(vel.x, rb.velocity.y);

        if (isGrounded)
        {
            if (x == 0) rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            if (x == 0) rb.velocity *= new Vector2(0.95f, 1);

            if (rb.velocity.y > 0 && !holding) rb.velocity *= new Vector2(1, 0.9f);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            jumpBuffer = 0.1f;
        }

        if (isGrounded)
        {
            if (jump)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);

                Vector2 jumpforce = (Vector3.up * Mathf.Sqrt(jumpHeight * -2.0f * Physics2D.gravity.y * rb.gravityScale));
                rb.velocity += jumpforce;

                jump = false;
                holding = true;
            }
        }
        else
        {
            if (Input.GetButtonUp("Jump") && holding)
            {
                holding = false;
            }
        }
    }
}
