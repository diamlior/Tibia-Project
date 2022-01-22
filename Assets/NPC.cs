using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Creature
{
    // Start is called before the first frame update

    void Start()
    {
        base.Start();
        battleWindow.AllCreatures.Add(this);

    }


}
