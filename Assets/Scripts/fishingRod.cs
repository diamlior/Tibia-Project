using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishingRod : Item
{
    bool fishingMode = false;
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
        if(fishingMode)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = this.transform.position.z;
            dragingCursor.transform.position = Input.mousePosition;
            if(Input.GetKeyDown(KeyCode.Mouse0) & !firstFrameEnteringMode)
            {
                fishingMode = false;
                Cursor.visible = true;
                dragingCursor.Visible(false);
                Debug.Log("Leaving Fishing Mode.");
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero, 0f);
                if(hit)
                {
                    if(hit.collider.gameObject.CompareTag("water"))
                    {
                        if (Random.Range(0f, 1f) > 0.8f)
                        {
                            GameObject newFish = Instantiate(fish, playermovement.realPos, Quaternion.identity);
                            Item FishItem = newFish.GetComponent<Item>();
                            playerObj.GetComponent<Inventory>().AddItem(newFish, FishItem.id, FishItem.type, FishItem.description, newFish.GetComponent<SpriteRenderer>().sprite);
                        }
                    }
                }
            }
            firstFrameEnteringMode = false;
        }
    }

    public override void UseItem()
    {
        fishingMode = true;
        Cursor.visible = false;
        dragingCursor.Visible(true);
        Debug.Log("Fishing rod click detected.");
        firstFrameEnteringMode = true;
    }


  
}
