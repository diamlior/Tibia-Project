using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsUp : UpDownFloor
{
    public override void Do(GameObject otherGameObject)
    {
        Vector3 newPos;
        if (otherGameObject.GetComponent<playerMovement>() != null)
        {
            playerMovement movement = otherGameObject.GetComponent<playerMovement>();
            newPos = movement.realPos;
            //   newPos.z = newPos.z + 11;
            newPos.x = newPos.x - 1000;
            newPos.y = newPos.y + 1;
            movement.realPos = newPos;
            otherGameObject.transform.position = newPos;
            Debug.Log("Stairs Up");
        }
        else
        {
            newPos = otherGameObject.transform.position;
      //      newPos.z = newPos.z + 11;
            newPos.x = newPos.x - 1000;
            newPos.y = newPos.y + 1;
            otherGameObject.transform.position = newPos;
            Debug.Log("Stairs Up");
        }
    }
}
