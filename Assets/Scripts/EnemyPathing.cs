using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    public WayPoints wayPoints;
   
   // public Transform waypointHolder;
    public float speed;

    private int wayPointIndex;
    private void Awake()
    {
        speed = GetComponent<Monster>().MovementSpeed;
    }
    void Start()
    {
        
        // wayPoints = GetComponent<WayPoints>();
        if (wayPoints != null)
        {
            

            Vector3[] waypoints = new Vector3[wayPoints.waypoints.Count];
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = wayPoints.waypoints[i].gameObject.transform.position;
                //waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
            }

            StartCoroutine(FollowPath(waypoints));
        }
        else
        {
            Debug.Log("cant find waypoints script");
        }
    }

    void Update()
    {
        
    }

    IEnumerator FollowPath(Vector3[] waypoints)
    {
        transform.position = waypoints[0];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        
     
        while (true)
        {
            transform.LookAt(targetWaypoint);
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
          
            Vector3 oldnewPositionDiffrence = targetWaypoint - transform.position;
            //Debug.Log(oldnewPositionDiffrence.magnitude);
            if (oldnewPositionDiffrence.magnitude < 0.5f)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                if (targetWaypointIndex >= waypoints.Length - 1)
                {
                    //Destroy(gameObject);
                    //Debug.Log("Destroy");
                }
               
            }
            
            yield return null;
        }
       
    }
}
