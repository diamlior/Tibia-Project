using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Haste : Spell
{
    public GameObject blessEffect;
    playerMovement movement;
    bool castable = true;
    public override void Cast()
    {
        if (castable)
        {
            if (enoughMana())
            {
                GameObject player = GameObject.Find("player");
                movement = player.GetComponent<playerMovement>();
                Instantiate(blessEffect, (Vector3)player.GetComponent<playerMovement>().realPos, Quaternion.identity);
                StartCoroutine(tempSpeed());
            }
        }
    }
    IEnumerator tempSpeed()
    {
        castable = false;
        float speed = movement.speed;
        movement.speed = movement.speed * 1.7f;
        yield return new WaitForSecondsRealtime(10f);
        movement.speed = speed;
        castable = true;
    }
}
