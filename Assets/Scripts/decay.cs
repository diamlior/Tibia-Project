using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decay : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite sr1;
    public Sprite sr2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DecayBody());
    }
    IEnumerator DecayBody()
    {
        yield return new WaitForSecondsRealtime(3);
        sr.sprite = sr1;
        yield return new WaitForSecondsRealtime(3);
        sr.sprite = sr2;
        yield return new WaitForSecondsRealtime(3);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
