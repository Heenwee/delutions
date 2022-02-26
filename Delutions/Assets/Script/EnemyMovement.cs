using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    GameManager gm;
    Collider2D thisCol;

    Transform player;

    public float speed, acceleration;
    Vector2 movement;

    public float partMove, partMoveSpeed;
    public Transform[] bodyParts;

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

        Visuals();
    }
    void FixedUpdate()
    {
        if(gm != null)
        {
            if (gm.otherside)
            {
                thisCol.isTrigger = false;
                rb.AddForce(movement * acceleration);
            }
            else
            {
                rb.velocity = Vector2.zero;
                thisCol.isTrigger = true;
            }
        }
        ClampVelocity();
    }

    void ClampVelocity()
    {
        Vector2 velocity = new Vector2(rb.velocity.x, 0);
        Vector2 vel = Vector2.ClampMagnitude(velocity, speed);
        rb.velocity = new Vector2(vel.x, rb.velocity.y);
    }

    void Visuals()
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].localPosition = Vector2.MoveTowards(   bodyParts[i].localPosition,
                                                                new Vector2((rb.velocity.x * partMove * i) / bodyParts.Length, bodyParts[i].localPosition.y),
                                                                partMoveSpeed * Time.deltaTime);
        }
    }
}
