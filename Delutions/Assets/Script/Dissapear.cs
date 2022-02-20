using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissapear : MonoBehaviour
{
    public string[] tags;
    public bool colDestroy;
    public AudioClip sound;
    public float volume = 1;
    AudioSource source;
    public GameObject[] effect;

    public float DissapearTime, dissapearTimeMult = 1f;

    public GameObject trailParticles;

    public float camShakeDur, camShakeMag;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Death), Random.Range(DissapearTime * dissapearTimeMult, DissapearTime));
        //Destroy(gameObject, Random.Range(DissapearTime * dissapearTimeMult, DissapearTime));

        if (sound != null)
        {
            gameObject.AddComponent(typeof(AudioSource));
            source = GetComponent<AudioSource>();
            source.volume = volume;
            source.spatialBlend = 0.1f;
            source.PlayOneShot(sound);
        }

        StartCoroutine(Camera.main.GetComponent<CamShake>().Shake(camShakeDur, camShakeMag));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(colDestroy)
        {
            foreach (string tag in tags)
            {
                if(col.CompareTag(tag))
                {
                    Death();
                }
            }
            if(tags.Length == 0)
            {
                Death();
            }
        }
    }
    void Death()
    {
        if (trailParticles != null)
        {
            trailParticles.transform.parent = null;
            trailParticles.GetComponent<ParticleSystem>().Stop();
            Destroy(trailParticles, 1f);
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (effect.Length != 0) Instantiate(effect[Random.Range(0, effect.Length)], transform.position, transform.rotation);
    }
}
