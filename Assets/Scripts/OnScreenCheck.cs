using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OnScreenCheck : MonoBehaviour
{
    public GameObject creature;
    public RandomGhoulWalk walkingScript;
    bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        creature = transform.GetChild(0).gameObject;
        walkingScript = creature.GetComponent<RandomGhoulWalk>(); 
    }

    // Update is called once per frame
    void Update()
    {
        GameObject check = creature;
        if (check == null)
            Destroy(this.gameObject);
        if (Math.Abs(walkingScript.realPos.x - walkingScript.playermovement.realPos.x) < 13 && Math.Abs(walkingScript.realPos.y - walkingScript.playermovement.realPos.y) < 7)
        {
            creature.SetActive(true);
            if(active == false)
            {
                active = true;
                walkingScript.Start();
            }
        }
        else
        {
            creature.SetActive(false);
            active = false;
        }
    }
}
