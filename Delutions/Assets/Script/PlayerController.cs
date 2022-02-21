using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public Rigidbody2D visualRb;

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
    public float jumpTorque;

    Transform currentTarget;
    public float maxDistToTarget;
    public GameObject grappleLine;
    public GameObject ballGrappleEffect, grappleEffect;
    LineRenderer lr;
    DistanceJoint2D joint;

    [Header("Shooting")]
    public Transform bulletPos;
    public float fireRate;
    float fireTime;
    public GameObject bullet;
    public float spread;
    public float bulletSpeed;
    public int dmg, knockBack;

    public GameObject spriteMask;

    [HideInInspector]
    public bool clamp = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = grappleLine.GetComponent<LineRenderer>();
        joint = GetComponent<DistanceJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, groundMask);
        Jump();

        if (jump) jumpBuffer -= Time.deltaTime;
        if (jumpBuffer <= 0) jump = false;

        Grapple();
        Attack();
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

    void Grapple()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Transform closestBall = FindClosestGP(mousePos).transform;
        if (Input.GetButtonDown("Fire2"))
        {
            if (Vector2.Distance(mousePos, closestBall.position) <= maxDistToTarget)
            {
                currentTarget = closestBall;
                Quaternion rot = Quaternion.LookRotation((currentTarget.position - transform.position).normalized, Vector3.forward) * Quaternion.Euler(0, -90, 0);
                Instantiate(ballGrappleEffect, currentTarget.position, rot);
                Instantiate(grappleEffect, transform.position, rot);
            }
        }
        if (Input.GetButtonUp("Fire2"))
        {
            currentTarget = null;
        }

        if (currentTarget != null) joint.connectedBody = currentTarget.GetComponent<Rigidbody2D>();
        else joint.connectedBody = rb;

        Effects();
    }
    void Effects()
    {
        if (currentTarget != null)
        {
            grappleLine.SetActive(true);
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, currentTarget.position);

            spriteMask.SetActive(true);
        }
        else
        {
            grappleLine.SetActive(false);

            spriteMask.SetActive(false);
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

                visualRb.AddTorque(Random.Range(-jumpTorque, jumpTorque));

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

    void BulletRot()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        bulletPos.eulerAngles = new Vector3(0, 0, Mathf.Atan2((point.y - bulletPos.position.y), (point.x - bulletPos.position.x)) * Mathf.Rad2Deg);
    }
    void Attack()
    {
        if (fireTime < 0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                if(currentTarget != null) Fire();
            }
        }
        BulletRot();

        fireTime -= Time.deltaTime;
    }
    void Fire()
    {
        var b = Instantiate(bullet, bulletPos.position, bulletPos.rotation);

        //b.transform.Rotate(0, 0, Random.Range(-spread / 2, spread / 2));
        b.GetComponent<Rigidbody2D>().velocity = bulletPos.right * bulletSpeed;
        b.GetComponent<Dmg>().dmg = dmg;
        b.GetComponent<Dmg>().knockBack = knockBack;

        fireTime = fireRate;
    }

    public GameObject FindClosestGP(Vector3 pos)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Grapple");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - pos;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
