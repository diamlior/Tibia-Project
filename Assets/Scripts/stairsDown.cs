using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stairsDown : UpDownFloor
{


    // Start is called before the first frame update
    public override void Do(GameObject otherGameObject)
    {
        Vector3 newPos;
        if (otherGameObject.GetComponent<playerMovement>() != null)
        {
            newPos = otherGameObject.GetComponent<playerMovement>().realPos;
            newPos.x = newPos.x + 1000;
            newPos.y = newPos.y - 1;
            otherGameObject.GetComponent<playerMovement>().realPos = newPos;
            otherGameObject.transform.position = newPos;
            Debug.Log("Stairs Down");
        }
        else
        {
            newPos = otherGameObject.transform.position;
            newPos.x = newPos.x + 1000;
            newPos.y = newPos.y - 1;
            otherGameObject.transform.position = newPos;
            Debug.Log("Stairs Down");
        }
    }
}
