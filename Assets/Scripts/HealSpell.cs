using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HealSpell : Spell
{
    public GameObject blessEffect;

    public override void Cast()
    {
        if (enoughMana())
        {
            Debug.Log("exura was activated.");
            GameObject player = GameObject.Find("player");
            player.GetComponent<playerStats>().IncreaseHealhBy(50);
            Instantiate(blessEffect, (Vector3)player.GetComponent<playerMovement>().realPos, Quaternion.identity);
        }
    }

}
