using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthText : MonoBehaviour
{
    Vector3 targetPos;

    void Start()
    {
        //target position is set to 1 sqm up.
        targetPos = transform.position;
        targetPos.y += 1f;
    }


    void Update()
    {

        //move HP towards target
        transform.position = Vector3.MoveTowards(transform.position, targetPos,Time.deltaTime*1.2f);
   
        //if HP text is on target, Destroy text.
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            Destroy(this.gameObject);
    }
}
