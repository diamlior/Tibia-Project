using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAllGhouls : Spell
{
    public override void Cast()
    {
        
        //find all creatures
        GameObject[] creatures = GameObject.FindGameObjectsWithTag("creature");
        foreach(GameObject creature in creatures)
        {
            //kill only Ghouls
            if (creature.name.Contains("Ghoul"))
            {
                deathScript ds = creature.GetComponent<deathScript>();
                ds.Die();
            }
        }
    }
}
