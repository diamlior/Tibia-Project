using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public GameObject inventory;
 
    public void changeEnable()
    {
        inventory.SetActive(!inventory.active);
    }
}
