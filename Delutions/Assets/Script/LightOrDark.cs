using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOrDark : MonoBehaviour
{
    public GameObject[] lights;
    public GameObject[] darks;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        foreach (GameObject obj in lights) obj.SetActive(!gm.otherside);
        foreach (GameObject obj in darks) obj.SetActive(gm.otherside);
    }
}
