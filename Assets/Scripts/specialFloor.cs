using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialFloor : MonoBehaviour
{

    public virtual void Do(GameObject gameobject)
    {
        Debug.Log("This function was not overrided!");
    }
}
