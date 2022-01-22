using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class energyfield : Spell
{
    public GameObject energyeffect;
    public override void Cast()
    {
        if (enoughMana())
        {
            playerMovement movement = GameObject.Find("player").GetComponent<playerMovement>();
            Vector3 pos = movement.realPos;
            Vector3 handle = movement.realPos;
            List<Transform> fields = new List<Transform>();

            //get position of energy field
            switch (movement.dir)
            {
                case 2:
                    pos = new Vector3(pos.x, pos.y - 4, pos.z);
                    handle = new Vector3(0, -1, 0);
                    break;


                case 4:
                    pos = new Vector3(pos.x - 4, pos.y, pos.z);
                    handle = new Vector3(-1, 0, 0);
                    break;


                case 6:
                    pos = new Vector3(pos.x + 4, pos.y, pos.z);
                    handle = new Vector3(1, 0, 0);
                    break;


                case 8:
                    pos = new Vector3(pos.x, pos.y + 4, pos.z);
                    handle = new Vector3(0, 1, 0);
                    break;
            }

            //create energy wave effect depends on direction

            //the "handle"
            fields.Add(Instantiate(energyeffect, (Vector3)movement.realPos + handle, Quaternion.identity).GetComponent<Transform>());
            fields.Add(Instantiate(energyeffect, (Vector3)movement.realPos + handle * 2, Quaternion.identity).GetComponent<Transform>());

            //the "hammer"
            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                    fields.Add(Instantiate(energyeffect, pos + new Vector3(i, j, 0), Quaternion.identity).GetComponent<Transform>());




            //go over each field and make damage if neccesary
            foreach (Transform t in fields)
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(t.position, Vector3.zero, 0f);
                foreach (RaycastHit2D hit in hits)
                {
                    GameObject collided = hit.collider.gameObject;
                    if (collided != null)
                    {

                        if (collided.tag == "creature")
                        {
                            Debug.Log("make Damage");
                            collided.GetComponent<Creature>().makeDamage(80, Color.magenta);
                        }
                    }

                }


            }
        }
    }
}
