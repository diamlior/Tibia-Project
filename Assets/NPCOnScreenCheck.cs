using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCOnScreenCheck : MonoBehaviour
{
    public GameObject creature;
    public RandomWalker walkingScript;
    public playerMovement playermovement;
    bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        creature = transform.GetChild(0).gameObject;
        walkingScript = creature.GetComponent<RandomWalker>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject check = creature;
        if (check == null)
        {
            Destroy(this.gameObject);
            return;
        }

        if (Mathf.Abs(walkingScript.realPos.x - playermovement.realPos.x) < 13 && Mathf.Abs(walkingScript.realPos.y - playermovement.realPos.y) < 7)
        {
            
            if (active == false)
            {
                creature.SetActive(true);
                active = true;
            }
        }
        else
        {
            if (active == true)
            {
                active = false;
                creature.SetActive(false);
            }
        }

    }
}
