using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class summonghoul : Spell
{
    public GameObject ghoul, effect, battleWindow;
    private Vector3 pos;
    public Transform playerPos;
    
    public override void Cast()
    {
        if (enoughMana())
        {
            pos = new Vector3(UnityEngine.Random.Range(playerPos.transform.position.x - 3f, playerPos.transform.position.x + 3f), UnityEngine.Random.Range(playerPos.transform.position.y - 3f, playerPos.transform.position.y + 3f), playerPos.transform.position.z);
            pos.x = (float)Math.Round(pos.x);
            pos.y = (float)Math.Round(pos.y);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.up, 0f);
            while (hit)
            {

                pos = new Vector3(UnityEngine.Random.Range(playerPos.transform.position.x - 3f, playerPos.transform.position.x + 3f), UnityEngine.Random.Range(playerPos.transform.position.y - 3f, playerPos.transform.position.y + 3f), playerPos.transform.position.z);
                pos.x = (float)Math.Round(pos.x);
                pos.y = (float)Math.Round(pos.y);
                hit = Physics2D.Raycast(pos, Vector2.up, 0.5f);
            }
            GameObject newGhoul = Instantiate(ghoul, pos, Quaternion.identity, GameObject.Find("Creatures").GetComponent<Transform>()); //create ghoul
            newGhoul.name = "Ghoul";
            newGhoul.transform.GetChild(0).position = newGhoul.transform.position;
            Instantiate(effect, pos, Quaternion.identity); //create effect
        }
    }
}
    