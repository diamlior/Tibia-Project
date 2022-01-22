using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class Slot : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public GameObject item;
    public Item itemObject;
    public bool empty = false;
    public int id;
    public string type, description;
    public Sprite icon;
    public Transform slotIconGO;
    public Sprite defaultSprite;
    public TMPro.TextMeshProUGUI amountText;
    bool drag = false;
    dragCursor dragCursor;


    public void Start()
    {
        slotIconGO = transform.GetChild(0);

        //Get the sprite of the slot Background
        defaultSprite = GetComponent<UnityEngine.UI.Image>().sprite;
    }
    private void Update()
    {
        if (!empty)
        {

            //if the slot is empty but not marked as empty, do EmptySlot()
            var c = item;
            if (c == null)
                EmptySlot();

            //Update the sprites if needed
            if (icon == null || defaultSprite == null)
            {
                Sprite sp = item.GetComponent<SpriteRenderer>().sprite;
                icon = sp;

                updateSlot();
            } 
            
            if(drag)
            {
                slotDragFunction();
            }
        }

        //disable amount text if slot is empty.
        else if(amountText!= null)
            amountText.gameObject.SetActive(false);
        
    }

    void slotDragFunction()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragCursor.transform.position = Input.mousePosition;

            //when releasing the mouse click

        if (Input.GetMouseButtonUp(0))
        {
            bool onInventory = false;
            GameObject inventory = null;
            if (IsPointerOverUIObject())
            {
                    //check if the release was over any kind of Inventory.

                PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
                eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
                foreach (RaycastResult r in results)
                    if (r.gameObject.name.Contains("Inventory"))
                    {
                        Debug.Log(r.ToString());
                        inventory = r.gameObject;
                        if (!inventory.GetComponent<InventoryReference>().reference.Equals(this.gameObject))
                        {
                            onInventory = true;
                            break;
                        }
                    }
            }

            if (onInventory)
            {
                //if it was released over an inventory, drop and add item to that inventory.

                GameObject temp = item;
                DropItem();
                inventory.GetComponent<InventoryReference>().reference.GetComponent<Inventory>().AddItem(temp);
            }
            else
            {
               

                Item temp = itemObject;
                DropItem();
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                mousePos.x = (int)Math.Round(mousePos.x);
                mousePos.y = (int)Math.Round(mousePos.y);
                mousePos.z = 1;
                temp.UpdateLocation(mousePos);
            }
            drag = false;
            Cursor.visible = true;
            dragCursor.Visible(false);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        if (!empty && drag == false)
        {
            Cursor.visible = false;
            Item slotItem = item.GetComponent<Item>();
            drag = true;
            dragCursor = slotItem.dragingCursor;
            dragCursor.Visible(true);
        }
    }


    public void OnPointerClick(PointerEventData pointerEventData)
    {
        
            if (!empty)
        {
            if (pointerEventData.button == PointerEventData.InputButton.Right)
            {
                DropItem();
            }
            else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                item.GetComponent<Item>().UseItem();
            }
        }

    }
    void EmptySlot()
    {
        item = null;
        itemObject = null;
        empty = true;
        icon = defaultSprite;
        

        updateSlot();
    }
    public void updateSlot()
    {
        slotIconGO.GetComponent<UnityEngine.UI.Image>().sprite = icon;
        if(amountText !=null && item != null)
        {
            amountText.gameObject.SetActive(true);
            int amount = item.GetComponent<Item>().amount;
            if (amount == 0)
                amountText.text = "";
            else
                amountText.text = "" + amount;
        }
   
    }


    void DropItem()
    {
        if(!empty)
        {
            Vector3 player = itemObject.playermovement.realPos;
            item.transform.position = player;
            item.transform.parent = GameObject.Find("ItemsOnFloor").transform;
            item.GetComponent<Item>().PickedUp = false;
            item.GetComponent<Item>().UpdateLocation(player);
            item.GetComponent<SpriteRenderer>().enabled = true;
            item.GetComponent<Collider2D>().enabled = true;
            EmptySlot();
        }
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
