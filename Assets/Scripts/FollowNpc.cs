using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FollowNpc : MonoBehaviour
{
    public GameObject playerTTFollow;
    Boolean started = false;
    public Color boxcolor = Color.green;
    
    void Start()
    {
       // transform.localScale = new Vector3(0, 0, 0);
      //  StartCoroutine(Timerstart());
    }

    IEnumerator Timerstart()
    {


        yield return new WaitForSecondsRealtime(3);
        started = true;

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            if (boxcolor == Color.green)
                boxcolor = Color.red;
            else
                boxcolor = Color.green;
    }
    private void FixedUpdate()
    {
        if (started)
        {
            transform.localScale = new Vector3((float)3.2, (float)3.2, 3);
            GetComponent<SpriteRenderer>().color = boxcolor;
            transform.position = (playerTTFollow.GetComponent<Transform>().position) - (new Vector3(0, (float)0.15, (float)2));
        }
    }
}
