using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform head;
    Transform player;

    public GameObject shootEffect;
    public GameObject hitEffect, playerHitEffect;

    public float fireRate, startFireTime;
    float fireTime;
    public float delay;

    public int dmg, knockBack;

    public LayerMask layerMask;

    Rigidbody2D rb;
    GameManager gm;
    Collider2D thisCol;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = GameManager.instance;
        thisCol = GetComponent<Collider2D>();
        player = GameObject.Find("Player").transform;

        fireTime = startFireTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.otherside)
        {
            if (fireTime <= 0) StartCoroutine(Shoot());

            fireTime -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (gm.otherside)
        {
            thisCol.isTrigger = false;
        }
        else
        {
            rb.velocity = Vector2.zero;
            thisCol.isTrigger = true;
        }
    }

    IEnumerator Shoot()
    {
        Debug.Log("Shoot");
        fireTime = fireRate;
        Vector2 toPlayer = player.position - head.position;

        yield return new WaitForSeconds(delay);

        if(gm.otherside)
        {
            RaycastHit2D hit = Physics2D.Raycast(head.position, toPlayer.normalized, Mathf.Infinity, layerMask);

            GameObject obj = hit.transform.gameObject;

            Quaternion effectRot = Quaternion.LookRotation(toPlayer.normalized) * Quaternion.Euler(0, -90, 0);

            Instantiate(shootEffect, head.position, effectRot);
            Instantiate(hitEffect, hit.point, effectRot * Quaternion.Euler(0, 0, 180));

            if (obj.CompareTag("Player"))
            {
                PlayerHp hp = obj.GetComponent<PlayerHp>();
                if (hp != null)
                {
                    hp.TakeDamage(dmg);

                    Instantiate(playerHitEffect, obj.transform.position, effectRot);
                }
            }
        }
    }
}
