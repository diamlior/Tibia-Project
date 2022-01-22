using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


// This class lets you move items on the floor.
// Not suitable for inheritance.
public class Item : MonoBehaviour
{
    protected bool draggable = true;
    public bool walkable = true;
    public Vector3 realPos;
    Vector3 start, end;
    public bool drag = false;
    public int id;
    public string type, description;
    public dragCursor dragingCursor;
    public playerMovement playermovement;
    public GameObject playerObj;
    public bool PickedUp = false;
    public int amount = 0;

    public virtual void Start()
    {
        playerObj = GameObject.Find("player");
        playermovement = playerObj.GetComponent<playerMovement>();
        realPos = this.GetComponent<Transform>().position;
        realPos.x = (int)(realPos.x);
        realPos.y = (int)(realPos.y);
        this.GetComponent<Transform>().position = realPos;
        if (dragingCursor == null)
            dragingCursor = GameObject.Find("dragCursor").GetComponent<dragCursor>();
        dragingCursor.Visible(false);
    }
    void OnMouseDrag()
    {
        if (draggable)
            if (!drag && !Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl) && inUseRange())
            {
                Debug.Log("Dragging");
                drag = true;
                Cursor.visible = false;
                dragingCursor.Visible(true);
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
    public virtual void Update()
    {
        if (drag)
        {
            dragFunction();

        }
        if (PickedUp)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }


    }

    public void dragFunction()
    {

        Vector3 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        endPos.z = realPos.z;
        dragingCursor.transform.position = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
        {
            bool onInventory = false;
            GameObject inventory = null;
            if (IsPointerOverUIObject())
            {

                PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
                eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
                foreach (RaycastResult r in results)
                    if (r.gameObject.name.Contains("Inventory"))
                    {
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
                inventory.GetComponent<InventoryReference>().reference.GetComponent<Inventory>().AddItem(this.gameObject);
            }
            else
            {
                endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                endPos.x = (int)Math.Round(endPos.x);
                endPos.y = (int)Math.Round(endPos.y);
                endPos.z = 1;
                UpdateLocation(endPos);
            }
            drag = false;
            Cursor.visible = true;
            dragingCursor.Visible(false);
        }
    }
    public virtual bool UpdateLocation(Vector3 newPos)
    {
        bool changed = false;
        newPos.z = realPos.z;
        RaycastHit2D[] hits = Physics2D.RaycastAll(newPos, newPos, 0.1f);
        foreach (RaycastHit2D hit in hits)
        {


            if (hit.collider.gameObject.layer == 8 && !hit.collider.CompareTag("hole"))
            {

                if (hit.collider.tag == "water")
                {
                    Destroy(this.gameObject);
                }


            }
            if (hit.collider.gameObject.GetComponent<UpDownFloor>() != null)
            {
                realPos = newPos;
                this.GetComponent<Transform>().position = realPos;
                hit.collider.gameObject.GetComponent<UpDownFloor>().Do(this.gameObject);
                realPos = transform.position;
                changed = true;
            }


        }
        realPos = newPos;
        this.GetComponent<Transform>().position = realPos;
        return changed;
    }

    public virtual void OnMouseDown()
    {
        if (inUseRange())
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {

                Inventory inv = playerObj.GetComponent<Inventory>();
                inv.AddItem(this.gameObject, id, type, description, GetComponent<SpriteRenderer>().sprite);

            }
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                UseItem();
        }

    }

    public bool inUseRange()
    {
        return ((Math.Abs(this.realPos.x - playermovement.realPos.x) <= 1 && Math.Abs(this.realPos.y - playermovement.realPos.y) <= 1) || PickedUp);
    }

    public virtual bool isWalkable()
    {
        return walkable;
    }
    public virtual void UseItem()
    {
        Debug.Log("this function was not overrided.");
    }
}
