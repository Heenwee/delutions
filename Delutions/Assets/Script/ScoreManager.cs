using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [HideInInspector]
    public int score;

    public Text scoreText;
    public Color baseColour, otherColour;

    GameManager gm;

    #region Singleton
    public static ScoreManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        scoreText.text = score.ToString();
        if (!gm.otherside) scoreText.color = baseColour;
        else scoreText.color = otherColour;
    }
}
