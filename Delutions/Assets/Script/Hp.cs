using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp : MonoBehaviour
{
    Rigidbody2D rb;

    public int hp;
    int currentHp;
    public int score;

    public GameObject hitEffect, deathEffect;
    Quaternion lastColRot;

    //public GameObject dmgText;
    //public float dmgTextRandom = 0.1f;

    [HideInInspector]
    public bool clamp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHp = hp;
        clamp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHp <= 0) Die();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Bullet"))
        {
            Dmg dmg = col.gameObject.GetComponent<Dmg>();
            if(dmg != null)
            {
                lastColRot = col.transform.rotation;

                Instantiate(hitEffect, transform.position, lastColRot);

                Debug.Log("Hit");
                currentHp -= dmg.dmg;
                rb.velocity = col.transform.right * dmg.knockBack;

                StartCoroutine(ReClamp());

                /*var txt = Instantiate(dmgText, transform.position, transform.rotation);

                txt.transform.SetParent(GameObject.Find("OverlayUI").transform);
                //txt.transform.position = new Vector3(txt.transform.position.x, txt.transform.position.y, 0);
                txt.transform.position += new Vector3(Random.Range(-dmgTextRandom, dmgTextRandom), Random.Range(-dmgTextRandom, dmgTextRandom));

                txt.transform.localScale = new Vector2(0.2f, 0.2f);
                Text text = txt.GetComponent<Text>();
                text.text = (col.GetComponent<Dmg>().dmg).ToString();*/
            }
        }
    }
    public IEnumerator ReClamp()
    {
        clamp = false;
        yield return new WaitForSeconds(0.1f);
        clamp = true;
    }
    void Die()
    {
        ScoreManager.instance.score += score;

        Instantiate(deathEffect, transform.position, transform.rotation);

        Transform[] children = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform c in children)
        {
            c.transform.parent = null;
            Rigidbody2D cRb = c.GetComponent<Rigidbody2D>();
            if (cRb != null) cRb.simulated = true;
            cRb.AddForce(Random.onUnitSphere * 100);
            cRb.AddTorque(Random.Range(-10f, 10f));

            RotateSprite rs = c.GetComponent<RotateSprite>();
            if (rs != null) rs.enabled = false;

            Dissapear d = c.GetComponent<Dissapear>();
            if (d != null) d.enabled = true;

            SpriteRenderer sr = c.GetComponent<SpriteRenderer>();
            if (sr != null) 
            {
                sr.color *= 0.8f;
                sr.sortingOrder -= 1;
            }
        }

        Destroy(gameObject);
    }
}
