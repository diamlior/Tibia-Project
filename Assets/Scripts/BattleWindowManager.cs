using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class BattleWindowManager : MonoBehaviour
{
    public GameObject battleWindow, battleItem;
    public List<Transform> creatureList = new List<Transform>();
    public List<Creature> AllCreatures = new List<Creature>();
    public GameObject[] AllNPC;
    public List<Creature> AllVisibleCreatures = new List<Creature>();
    GameObject player;

    int frameCounter = 20;
    private void Start()
    {
        player = GameObject.Find("player");
    }

    private void Update()
    {
        if (frameCounter == 25)
        {
            


            foreach (Creature creature in AllCreatures)
            {
                //If a creatures is on screen but not in AllVisibleCreatures list, add it.
                if (!AllVisibleCreatures.Contains(creature) && onScreenCheck(creature.transform))
                {
                    AddCreatureToBattle(creature);
                }
                
                //If a creature is not on screen but is in the list, remove it.
                if (!onScreenCheck(creature.transform) && AllVisibleCreatures.Contains(creature))
                    RemoveCreatureFromBattle(creature);
            }

            foreach (Creature creature in AllVisibleCreatures) // Delete all dead creatures from Battle Window
                if (creature == null || !creature.isAlive())
                {
                    foreach (Transform tempCreature in battleWindow.transform)
                    {
                        if (tempCreature.GetComponent<battleCreature>().creature.Equals(creature))
                            Destroy(tempCreature.gameObject);
                            
                    }
                }


            if(AllVisibleCreatures.Count>0) // Clean all dead creatures from "AllVisible" List.
               foreach (var c in AllVisibleCreatures)
                {
                    if (c == null)
                    {
                        AllVisibleCreatures.Remove(c);
                        break;
                    }
                }

            
            foreach(battleCreature creature in battleWindow.transform.GetComponentsInChildren<battleCreature>())
            {
                if (creature.creature.active == false)
                {
                    foreach(Creature creatureInList in AllVisibleCreatures)
                        if (creature.creature.Equals(creatureInList))
                        {
                            AllVisibleCreatures.Remove(creatureInList);
                            break;
                        }
                    Destroy(creature.gameObject);
                }
                    
            }
            frameCounter = 0;
        }
        frameCounter++;
    }

    public void AddCreatureToBattle(Creature inputCreature)
    {
        AllVisibleCreatures.Add(inputCreature);
        GameObject newCreature = Instantiate(battleItem, battleWindow.transform);
        battleCreature newCreaturesData = newCreature.GetComponent<battleCreature>();
        newCreaturesData.creature = inputCreature.gameObject;
        newCreaturesData.setName(inputCreature.name);
        Sprite sp = inputCreature.GetMainSprite();
        newCreaturesData.setImage(sp);
    }


    public void RemoveCreatureFromBattle(Creature creatureToDelete)
    {
        AllVisibleCreatures.Remove(creatureToDelete);
        foreach (Transform creature in battleWindow.transform)
        {
            if (creature.GetComponent<battleCreature>().creature.Equals(creatureToDelete))
                Destroy(creature.gameObject);
        }
        
    }

    bool onScreenCheck(Transform creature)
    {
        if(Math.Abs(creature.position.x - player.transform.position.x)<13 && Math.Abs(creature.position.y - player.transform.position.y) < 7)
          return true;
        return false;
    }
}
