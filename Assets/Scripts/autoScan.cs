using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class autoScan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ScanGraph());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ScanGraph()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        AstarPath.active.Scan();
        StartCoroutine(ScanGraph());
    }
}
