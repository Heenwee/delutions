using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;

    public float startSpawnRate, spawnRateIncrease;
    float spawnTime;
    public float spawnBounds;
    public LayerMask layerMask;

    public int baseEnemyCount, enemyIncrease;
    int enemiesSpawned;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;

        if (gm.currentEnemyMaxCount == 0) gm.currentEnemyMaxCount = baseEnemyCount;
        else gm.currentEnemyMaxCount += enemyIncrease;

        if (gm.spawnRate == 0) gm.spawnRate = startSpawnRate;
        else gm.spawnRate *= spawnRateIncrease;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTime <= 0 && gm.otherside) Spawn();

        spawnTime -= Time.deltaTime;
    }

    void Spawn()
    {
        if(enemiesSpawned < gm.currentEnemyMaxCount)
        {
            gm.currentEnemyCount++;
            enemiesSpawned++;

            spawnTime = gm.spawnRate;
            float point = Random.Range(-spawnBounds, spawnBounds);

            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(point, 0), -Vector2.up, Mathf.Infinity, layerMask);
            Instantiate(enemies[Random.Range(0, enemies.Length)], hit.point + new Vector2(0, 1), transform.rotation);
            if (enemiesSpawned >= gm.currentEnemyMaxCount) gm.allEnemiesSpawned = true;
        }
    }
}
