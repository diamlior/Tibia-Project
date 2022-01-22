using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Corpse : Item
{
    public GameObject inventoryCanvas;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        inventoryCanvas.SetActive(false);
        inventoryCanvas.transform.parent = GameObject.Find("InventoriesGrid").transform;
        if (inventoryCanvas == null)
            inventoryCanvas = this.GetComponent<Inventory>().inventory;
        inventoryCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if ((Math.Abs(playermovement.realPos.x - realPos.x) > 1 || Math.Abs(playermovement.realPos.y - realPos.y) > 1)&&!PickedUp)
            inventoryCanvas.SetActive(false);
    }

    public override void UseItem()
    {
        inventoryCanvas.SetActive(!inventoryCanvas.active);
    }
}
