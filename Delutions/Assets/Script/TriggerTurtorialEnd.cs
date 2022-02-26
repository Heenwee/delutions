using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTurtorialEnd : MonoBehaviour
{
    public Animator anim;

    private void OnDestroy()
    {
        anim.SetTrigger("end");
    }
}
