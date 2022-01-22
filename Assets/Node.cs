using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3 worldPos;

    public Node(bool _Walkable, Vector3 _worldPos)
    {
        walkable = _Walkable;
        worldPos = _worldPos;
    }
}
