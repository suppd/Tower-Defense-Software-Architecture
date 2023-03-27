using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public List<GameObject> waypoints = new List<GameObject>();     
        void Awake()
        {
            foreach (Transform t in transform)
            {
                waypoints.Add(t.gameObject);
            }
        }
}
