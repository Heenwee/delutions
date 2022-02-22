using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;

    public float spawnRate;
    float spawnTime;
    public float spawnBounds;
    public LayerMask layerMask;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTime <= 0 && gm.otherside) Spawn();

        spawnTime -= Time.deltaTime;
    }

    void Spawn()
    {
        spawnTime = spawnRate;
        float point = Random.Range(-spawnBounds, spawnBounds);

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(point, 0), -Vector2.up, Mathf.Infinity, layerMask);
        Instantiate(enemies[Random.Range(0, enemies.Length)], hit.point + new Vector2(0, 1), transform.rotation);
    }
}
