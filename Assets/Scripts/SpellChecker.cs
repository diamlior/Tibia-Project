using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellChecker : Spell
{
    public override bool isSpell(string name)
    {
        foreach (Transform child in transform)
            if (child.name == name)
            {
                if (child.gameObject.active == true)
                {
                    Debug.Log("Spell is active");
                    child.GetComponent<Spell>().Cast();
                    return true;
                }
            }
        return false;
    }
}
