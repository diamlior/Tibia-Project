using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackCreature : MonoBehaviour
{
    public GameObject redSquare;
    public bool isAttacking = false;
    public GameObject square;
    public Creature attackedCreature;
    GameObject player;
    
    private void Start()
    {
        player = GameObject.Find("player");
    }
    private void Update()
    {
        
        // mouse clicked
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftAlt))
        {
            
            // get mouse position's SQM
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z = player.transform.position.z;
            mousepos = new Vector3((float)Math.Round(mousepos.x), (float)Math.Round(mousepos.y), (float)Math.Round(mousepos.z));
          


            //Check which creature is in the same SQM
           GameObject[] creatures = GameObject.FindGameObjectsWithTag("creature");
            foreach (GameObject creature in creatures)
            {
                if (creature.name != "player")
                {
                    RandomGhoulWalk rgw = creature.GetComponent<RandomGhoulWalk>();

                    //if creature is in mouse position
                    if (rgw.realPos.x == mousepos.x && rgw.realPos.y == mousepos.y)
                    {
                        startAttack(creature);
                    }
                }

            }
        }

        
        if (isAttacking)
        {
            //check if attacked creature is dead
            if (!attackedCreature.isAlive())
            {
                //destroy red square
                redSquare.GetComponent<RedSquareFollow>().active = false;
                Destroy(square.gameObject); ;
                isAttacking = false;
            }
            
        }


            //make sure that attackedCreature reference is not null
        Creature r = attackedCreature;
        if (r != null)
        {
            if (r.gameObject.active == false)
                isAttacking = false;
        }

    }
    public void startAttack(GameObject creature)
    {
        redSquare.GetComponent<RedSquareFollow>().active = true;
        //redSquare does'nt exist yet
        if (!isAttacking)
        {

            square = Instantiate(redSquare, creature.transform.position, Quaternion.identity);
            isAttacking = true;

        }

        //if redSquare exist
        if (square != null)
        {

            RedSquareFollow script = square.GetComponent<RedSquareFollow>();

            //if pressed on redSquare again - stop attacking
            if (script.playerToFollow == creature)
            {
                redSquare.GetComponent<RedSquareFollow>().active = false;
                Destroy(square.gameObject); ;
                isAttacking = false;
            }

            //else - start attacking
            else
            {
                script.playerToFollow = creature;
                attackedCreature = creature.GetComponent<Creature>();
            }
        }
    }
}

