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

    bool reload;

    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    private void Start()
    {
        reload = false;
        allEnemiesSpawned = false;
    }

    private void Update()
    {
        if(currentEnemyCount <= 0 && allEnemiesSpawned)
        {
            if (!reload)
            {
                Reload();
                reload = true;
            }
        }
    }

    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        reload = false;
        allEnemiesSpawned = false;
    }
}
