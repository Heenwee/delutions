using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start ()
    {
        GameManager.instance.ReloadVariables();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
