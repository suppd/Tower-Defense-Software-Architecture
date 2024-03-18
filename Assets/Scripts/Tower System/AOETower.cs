using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOETower : TowerBase
{
    //Normal Tower Specific variables like damage increase
    [SerializeField]
    private float radiusIncrease = 1;
    protected override void UpgradeSpecifics()
    {
        Debug.Log("Old AOE Tower Radius is : " + this.ExplosionRadius);
        this.ExplosionRadius += radiusIncrease;
        Debug.Log("New AOE Tower Radius is : " + this.ExplosionRadius);
    }
}
