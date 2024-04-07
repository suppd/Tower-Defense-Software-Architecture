using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This Class is put on an empty gameobject in the scene and then you can drag all the below child WayPoint prefabs into it to add it to a single waypoints list
/// and then use this list for the enemy pathing for example
/// </summary>
public class WayPoints : MonoBehaviour
{
    public List<GameObject> Waypoints = new List<GameObject>();     
        void Awake()
        {
            foreach (Transform t in transform)
            {
                Waypoints.Add(t.gameObject);
            }
        }
}
