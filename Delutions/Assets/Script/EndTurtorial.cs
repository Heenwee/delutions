using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTurtorial : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene("SampleScene");
        SceneManager.LoadScene("Game Management", LoadSceneMode.Additive);
    }
}
