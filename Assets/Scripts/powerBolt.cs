using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerBolt : MonoBehaviour
{
    private Transform targetTransform;
    public GameObject target;
    public int damage = 20;
    public float speed;
    private void Start()
    {
        //if bolt has target, get target's position
        if (target != null && target.GetComponent<Creature>() != null)
            targetTransform = target.GetComponent<Transform>();
    }
    void FixedUpdate()
    {

        //if bolt is missing target, Destroy.
        if (target == null || target.GetComponent<Creature>() == null)
            Destroy(this.gameObject);
        
        else
        {

            //rotate bolt towards creature
            Vector3 vectorToTarget = targetTransform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle - 45, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1000);

            //bolt follow creature position
            transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed);

            //if bolt less than 0.3sqm - make damage and destroy bolt
            if (Vector3.Distance(transform.position, targetTransform.position) < 0.3)
            { 
                damage = 40;
                target.GetComponent<Creature>().makeDamage(damage, Color.red);
                Destroy(this.gameObject);
            }


        }

    }
}
