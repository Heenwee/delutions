using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [HideInInspector]
    public bool otherside, allEnemiesSpawned;
    [HideInInspector]
    public int currentEnemyCount, currentEnemyMaxCount;
    [HideInInspector]
    public float spawnRate;

    public GameObject[] maps;
    int currentMap;

    bool reloaded;

    Animator anim;

    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
        allEnemiesSpawned = false;
        reloaded = false;
        currentMap = 0;
        Shuffle();
    }

    public void ReloadVariables()
    {
        Debug.Log("Reload");
        allEnemiesSpawned = false;
        reloaded = false;

        if (currentMap < maps.Length - 1) currentMap++;
        else
        {
            currentMap = 0;
            Shuffle();
        }
        Instantiate(maps[currentMap]);
        //anim.SetTrigger("in");
    }

    private void Update()
    {
        if(currentEnemyCount <= 0 && allEnemiesSpawned) StartReload();
    }

    public void StartReload()
    {
        if(!reloaded) 
        {
            anim.SetTrigger("out");
            reloaded = true;
        }
    }
    void Reload()
    {
        allEnemiesSpawned = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Shuffle()
    {
        for (int i = 0; i < maps.Length - 1; i++)
        {
            int rnd = Random.Range(i, maps.Length);
            GameObject temp = maps[rnd];
            maps[rnd] = maps[i];
            maps[i] = temp;
        }
    }
}
