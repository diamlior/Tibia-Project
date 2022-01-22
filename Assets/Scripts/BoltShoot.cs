using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoltShoot : MonoBehaviour
{
    public Transform player;
    public GameObject bolt;
    public attackCreature attackScript;
    private GameObject redSquare;
    private bool active = false;
    private void Start()
    {
        StartCoroutine(startShooting());
    }
    void Update()
    {
        active = attackScript.isAttacking;
        if(active)
          redSquare = attackScript.square;
    }
    IEnumerator startShooting()
    {
        //if isAttacking and redSquare Detected.
        if (active && redSquare != null)
        {
            //create bolt
            GameObject newBolt = Instantiate(bolt, player.transform.position, Quaternion.identity); //create bolt
       
            //set bolt's target to redSquare's target.
            newBolt.GetComponent<powerBolt>().target = redSquare.GetComponent<RedSquareFollow>().playerToFollow;
        }
        yield return new WaitForSecondsRealtime(1);
        StartCoroutine(startShooting());
    }
}
