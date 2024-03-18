using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTower : TowerBase
{
    //Normal Tower Specific variables like damage increase
    [SerializeField]
    private int damageIncrease = 1;
    protected override void UpgradeSpecifics()
    {
        Debug.Log("Old Normal Tower Damage is : " + this.Damage);
        this.Damage += damageIncrease;
        Debug.Log("New Normal Tower Damage is : " + this.Damage);
    }
}

