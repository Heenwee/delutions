using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    GameManager gm;
    Collider2D thisCol;

    Transform player;

    public float speed, aceleration;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = GameManager.instance;
        thisCol = GetComponent<Collider2D>();

        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 toOther = player.position - transform.position;
        Vector2 mov = new Vector2(toOther.x, 0);
        mov.Normalize();

        movement = mov;
    }
    void FixedUpdate()
    {
        if(gm.otherside)
        {
            thisCol.isTrigger = false;
        }
        else
        {
            rb.velocity = Vector2.zero;
            thisCol.isTrigger = true;
        }

        ClampVelocity();
    }

    void ClampVelocity()
    {
        Vector2 velocity = new Vector2(rb.velocity.x, 0);
        Vector2 vel = Vector2.ClampMagnitude(velocity, speed);
        rb.velocity = new Vector2(vel.x, rb.velocity.y);
    }
}
