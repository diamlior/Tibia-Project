using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemyAI : MonoBehaviour
{


    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public playerMovement playerMovement;

    Path path;
    int currentWaypoint = 0;

    public List<Vector3> waypoints;

    Seeker seeker;
    Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        position = this.transform.position;

    //    InvokeRepeating("UpdatePath", 0f, 0.5f);
        
        
    }

    public void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(this.transform.position, playerMovement.realPos, OnPathComplete);
    }



    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public Vector2 getNextStep()
    {
        if(path.vectorPath.Count>0)
          return path.vectorPath[1];
        return new Vector2(0,0);
    }
   
    void FixedUpdate()
    {
        if (path == null || currentWaypoint >= path.vectorPath.Count)
            return;



        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)this.transform.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        float distance = Vector2.Distance(this.transform.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        waypoints = path.vectorPath;
    }
}
