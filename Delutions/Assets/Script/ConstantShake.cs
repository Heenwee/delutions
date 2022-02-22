using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantShake : MonoBehaviour
{
    Vector3 originalPos;
    public float magnitude;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.localPosition;
        gm = GameManager.instance;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (gm.otherside)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos;
            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
        }
        else transform.localPosition = originalPos;
    }
}
