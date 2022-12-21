using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTriggerScript : MonoBehaviour
{
    public bool enemyEntered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            enemyEntered = true;
            Destroy(other.gameObject);
        }
    }
}
