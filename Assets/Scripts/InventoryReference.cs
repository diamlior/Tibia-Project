using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryReference : MonoBehaviour
{
    public GameObject reference;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(counter==30)
        {
            GameObject test = reference;
            if (test == null)
            {
                Debug.Log("Destroying " + this.gameObject.name + " because reference is null.");
                Destroy(this.transform.parent.gameObject);
            }
            counter = 0;
        }
        counter++;
    }
}
