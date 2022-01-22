using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSquareFollow : MonoBehaviour
{
    public GameObject playerToFollow;
    public bool active = true;
    public Color boxcolor = Color.red; //the color of the square

    Creature creature;

    private void Start()
    {
        //get Creature component
        creature = playerToFollow.GetComponent<Creature>();
        transform.localScale = new Vector3((float)3.2, (float)3.2, 3);
        GetComponent<SpriteRenderer>().color = boxcolor;
    }
    private void FixedUpdate()
    {
        //check if creature is dead
        if (!creature.isAlive() || creature.gameObject.active == false)
            if (boxcolor == Color.red)
                this.gameObject.active = false;
            else
                Destroy(this.gameObject);
        else
        {
            //follow creature 
            transform.position = (playerToFollow.GetComponent<Transform>().position) - (new Vector3(0, (float)0.15, (float)0));
        }
    }



}
