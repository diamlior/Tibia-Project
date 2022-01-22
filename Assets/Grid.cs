using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public RandomGhoulWalk walk;
    public Vector3 gridWorldSize;
    public LayerMask unwalkable;
    Node[,] grid;

    int gridSizeX, gridSizeY;
    private void Start()
    {
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y);
        createGrid();
    }

    void createGrid()
    {
        Vector3 bottomLeft = walk.realPos - Vector3.up * gridSizeY / 2 - Vector3.right * gridSizeX / 2;
        grid = new Node[gridSizeX, gridSizeY];
        for(int x = 0; x<gridSizeX;x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * x + Vector3.up * y;
                bool walkable = !(Physics.CheckSphere(worldPoint, 0.2f));
                grid[x, y] = new Node(walkable, worldPoint);
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(walk.realPos, new Vector3(gridWorldSize.x, gridWorldSize.y));
        if(grid !=null)
            foreach(Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPos, Vector3.one*0.9f);
            }
    }
}
