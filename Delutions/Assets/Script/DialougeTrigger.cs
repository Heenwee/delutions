using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeTrigger : MonoBehaviour
{
    public Dialouge dialouge;
    public float startTime;

    private void Start()
    {
        Invoke(nameof(TriggerDialouge), startTime);
    }

    public void TriggerDialouge()
    {
        DialougeManager.instance.StartDialouge(dialouge);
    }
}
