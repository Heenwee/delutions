using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp : MonoBehaviour
{
    Rigidbody2D rb;

    public int hp;
    int currentHp;

    public GameObject hitEffect, deathEffect;
    Quaternion lastColRot;

    //public GameObject dmgText;
    public float dmgTextRandom = 0.1f;

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
        if(col.CompareTag("Spell"))
        {
            Dmg dmg = col.gameObject.GetComponent<Dmg>();
            if(dmg != null)
            {
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

                lastColRot = col.transform.rotation;
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
        Instantiate(deathEffect, transform.position, lastColRot);
        Destroy(gameObject);
    }
}
