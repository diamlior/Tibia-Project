using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    bool inventoryEnabled = true;


    private int allSlots, enabledSlots;
    public GameObject[] slots;
    int counter = 0;

    public void Start()
    {
        allSlots = inventory.transform.childCount;
        
        slots = new GameObject[allSlots];
        for (int i = 0; i < allSlots; i++)
        {

            slots[i] = inventory.transform.GetChild(i).gameObject;
            if (slots[i].CompareTag("slot"))
            {
                Slot currentSlot = slots[i].GetComponent<Slot>();
                if (currentSlot.item == null)
                    currentSlot.empty = true;
                currentSlot.Start();
                counter++;
            }
        }

        GameObject[] tempAllslots = new GameObject[counter];
        for(int i = 0; i<counter;i++)
        {
            tempAllslots[i] = slots[i];
        }
        slots = tempAllslots;
        counter = 0;

    }

    public void AddItem(GameObject itemObject, int itemId, string type, string description, Sprite img)
    {
        for(int i = 0;i<allSlots;i++)
            if(slots[i].GetComponent<Slot>().empty)
            {
                Slot item = slots[i].GetComponent<Slot>();
                item.item = itemObject;
                item.id = itemId;
                item.type = type;
                item.description = description;
                item.icon = img;
                item.empty = false;
                item.itemObject = item.item.GetComponent<Item>();
                item.itemObject.PickedUp = true;

                item.item.transform.parent = slots[i].transform;
                item.updateSlot();
                break;
            }
    }

    public void AddItem(GameObject itemObject)
    {
        for (int i = 0; i < allSlots; i++)
            if (slots[i].GetComponent<Slot>().empty)
            {
                Slot item = slots[i].GetComponent<Slot>();
                item.item = itemObject;
                item.id = 0;
                item.type = "";
                item.description = "";
                item.icon = itemObject.GetComponent<SpriteRenderer>().sprite;
                item.empty = false;
                item.itemObject = item.item.GetComponent<Item>();
                item.itemObject.PickedUp = true;
                item.item.transform.parent = slots[i].transform;
                item.updateSlot();
                break;
            }
    }

}
