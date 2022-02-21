using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCollision : MonoBehaviour
{
    LayerMask layerMask;
    Vector3 prevPos;
    //Vector3 newPos;

    private void Start()
    {
        layerMask = Physics2D.GetLayerCollisionMask(gameObject.layer);

        prevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookAt = prevPos;
        //transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((lookAt.y - transform.position.y), (lookAt.x - transform.position.x)) * Mathf.Rad2Deg);

        float betw = Vector2.Distance(transform.position, prevPos);

        RaycastHit2D hit = Physics2D.Raycast(prevPos, transform.right, betw, layerMask);
        Debug.DrawLine(transform.position, transform.position + transform.right * betw, Color.red);

        if (hit.transform != null)
        {
            transform.position = hit.point;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
        prevPos = transform.position;
    }
}
