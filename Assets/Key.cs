using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Key : Item
{
    bool inUse = false;
    bool firstFrameEnteringMode = false;
    public GameObject fish;
    public Item mainItemInfo;
    playerMovement playerT;
    // GameObject playerOb;

    // Update is called once per frame
    public override void Start()
    {
        //Get all default info from MainItemInfo
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        if (inUse)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = this.transform.position.z;
            dragingCursor.transform.position = Input.mousePosition;
            if (Input.GetKeyDown(KeyCode.Mouse0) & !firstFrameEnteringMode)
            {
                inUse = false;
                Cursor.visible = true;
                dragingCursor.Visible(false);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero, 0f);
                if (hit)
                {
                    if (hit.collider.gameObject.CompareTag("door"))
                    {
                        DoorOpen door = hit.collider.gameObject.GetComponent<DoorOpen>();
                        if(Math.Abs(door.realPos.x - playermovement.realPos.x)<=1 && Math.Abs(door.realPos.y - playermovement.realPos.y) <= 1)
                            door.Unlock(id);
                    }
                }
            }
            firstFrameEnteringMode = false;
        }
    }

    public override void UseItem()
    {
        inUse = true;
        Cursor.visible = false;
        dragingCursor.Visible(true);
        firstFrameEnteringMode = true;
    }
}