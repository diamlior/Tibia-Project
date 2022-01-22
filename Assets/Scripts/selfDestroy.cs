using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(desroyText());
    }

    IEnumerator desroyText()
    {
        yield return new WaitForSecondsRealtime(3);
        Destroy(this.gameObject);
    }
}
