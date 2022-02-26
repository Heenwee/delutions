using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public int hp;
    [HideInInspector]
    public int currentHp;

    public float hitCooldown;
    float hitTime;

    public GameObject deathEffect;
    public GameObject hitEffect;

    public Text hpText;

    public static bool dead;
    public GameObject grappleObject;

    public Animator anim;

    private void Start()
    {
        currentHp = hp;
        dead = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Dmg>() != null)
        {
            if (hitTime <= 0)
            {
                currentHp -= col.gameObject.GetComponent<Dmg>().dmg;
                Instantiate(hitEffect, transform.position, transform.rotation);
                hitTime = hitCooldown;
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        currentHp -= dmg;
    }

    private void Update()
    {
        if (currentHp <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(grappleObject);
            GameManager.instance.DeathReload();
            anim.SetTrigger("death");
        }
        hitTime -= Time.deltaTime;

        hpText.text = currentHp.ToString();
    }
}
