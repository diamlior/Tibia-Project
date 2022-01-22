using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleUp : specialFloor
{
    GameObject player;
    playerMovement playerMovement;
    Vector2 thisRealPos;
    private void Start()
    {
        player = GameObject.Find("player");
        playerMovement = player.GetComponent<playerMovement>();
        thisRealPos.x = (float)Math.Round(transform.position.x);
        thisRealPos.y = (float)Math.Round(transform.position.y);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
            if ((Math.Abs(playerMovement.realPos.x - thisRealPos.x) <= 1.1) && Math.Abs(playerMovement.realPos.y - thisRealPos.y) <= 1.1)
                if (playerMovement.state == 5 || playerMovement.state == 0)
                {
                    Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousepos = new Vector2((float)Math.Round(mousepos.x), (float)Math.Round(mousepos.y));
                    Vector2 realPos = new Vector2((float)Math.Round(transform.position.x), (float)Math.Round(transform.position.y));
                    if (mousepos.Equals(realPos))
                    {
                        Vector3 newPos;
                        newPos = player.GetComponent<playerMovement>().realPos;
                        newPos.x = thisRealPos.x - 1000;
                        newPos.y = thisRealPos.y - 1;
                        player.GetComponent<playerMovement>().realPos = newPos;
                        player.transform.position = newPos;
                    }
                }
      
    }
}
