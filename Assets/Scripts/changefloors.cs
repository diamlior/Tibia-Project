using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changefloors : Spell
{
    public playerMovement playermove;

    public override void Cast()
    {
        Vector3 pos = playermove.realPos;
        if (pos.z == 1)
            pos.z = -10;
        else
            pos.z = 1;
        playermove.realPos = pos;
        playermove.gameObject.transform.position = pos;
    }
}
