using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
