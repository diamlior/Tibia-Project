
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position+offset;
    }
}
