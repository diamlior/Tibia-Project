using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitRealSeconds : MonoBehaviour
{
   
    void Start()
    {
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
        print(Time.time);
        yield return new WaitForSecondsRealtime(5);
        print(Time.time);
    }
}

