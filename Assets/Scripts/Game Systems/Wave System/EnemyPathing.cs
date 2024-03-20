using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    //this class is responsible for letting the enemies follow the given waypoints so the level designer can create a set path
    public WayPoints WayPoints;
    private float speed;
    private void Awake()
    {
        //set the speed of the monster with the given individual speed 
        speed = GetComponent<Monster>().MovementSpeed;
    }
    void Start()
    {
        // get the path to follow and then set it to the parent (enemy)
        if (WayPoints != null)
        {
            Vector3[] waypoints = new Vector3[WayPoints.Waypoints.Count];
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = WayPoints.Waypoints[i].gameObject.transform.position;
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
        speed = GetComponent<Monster>().MovementSpeed;
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
