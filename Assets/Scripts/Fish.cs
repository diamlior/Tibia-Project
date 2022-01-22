using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Item
{
    public Item mainItemInfo;
    // Start is called before the first frame update
    public override void Start()
    {
        //Get all default info from MainItemInfo
        base.Start();
    }

    // Update is called once per frame
    public override void UseItem()
    {
        playerStats stats = playermovement.GetComponent<playerStats>();
        stats.DecreaseManaBy(-20);
        Debug.Log("fish was eaten");
        Destroy(this.gameObject);
    }

}
