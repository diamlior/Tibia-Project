using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorOpen : Item
{
    public Sprite openSprite, closedSprite;
    bool isOpen = false;
    Collider2D doorCollider;
    SpriteRenderer sr;
    public bool unlocked = true;
    public GameObject lockedDoorText;
    // Start is called before the first frame update
    void Start()
    {
        draggable = false;
        sr = this.GetComponent<SpriteRenderer>();
        playerObj = GameObject.Find("player");
        playermovement = playerObj.GetComponent<playerMovement>();
        realPos = this.GetComponent<Transform>().position;
        realPos.x = (int)(realPos.x);
        realPos.y = (int)(realPos.y);
        sr.sprite = closedSprite;
        walkable = false;
        if (dragingCursor == null)
            dragingCursor = GameObject.Find("dragCursor").GetComponent<dragCursor>();
        dragingCursor.Visible(false);
    }

    // Update is called once per frame
    
    void Update()
    {
    }

    public override void UseItem()
    {
        if (unlocked)
        {
            if (isOpen)
            {
                sr.sprite = closedSprite;
                isOpen = false;
                walkable = false;
            }
            else
            {
                sr.sprite = openSprite;
                isOpen = true;
                walkable = true;
            }
              
            
            
        }
        else
            Instantiate(lockedDoorText, GameObject.Find("ScreenUI").transform);
    }

    public void Unlock(int id)
    {
        if (this.id == 0 || this.id == id)
        {

            unlocked = !unlocked;
            if (!unlocked)
            {
                sr.sprite = closedSprite;
                walkable = false;
            }
        }
        else
        {
            GameObject text = Instantiate(lockedDoorText, GameObject.Find("ScreenUI").transform);
            text.GetComponent<TMPro.TextMeshProUGUI>().text = "This key won't open the door.";
        }
        
    }
}
