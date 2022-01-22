using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Crate : Item
{
    public Item mainItemInfo;
    public override void Start()
    {
        //Get all default info from MainItemInfo
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }
    public override void UseItem()
    {
        Debug.Log("this crate is opened");
    }



}
