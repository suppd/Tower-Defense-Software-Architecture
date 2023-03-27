using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTriggerScript : MonoBehaviour
{
    /// <summary>
    /// this script sends event data to all eventbus subscribers once an enemy enters the player's base
    /// </summary>

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enemy entered the base!");
        GlobalBus.sync.Publish(this, new BaseEnterEvent(other)); //the enemy has entered the base (send event!!!) 
    }
  
}
