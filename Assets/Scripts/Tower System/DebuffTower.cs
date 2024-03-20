using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffTower : TowerBase
{
    //Normal Tower Specific variables like damage increase
    [SerializeField]
    private int slowIncrease = 1;
    protected override void UpgradeSpecifics()
    {
        Debug.Log("Old Debuff Tower Duration is : " + this.SlowDuration);
        this.SlowDuration += slowIncrease;
        Debug.Log("Old Debuff Tower Duration is : " + this.SlowDuration);
    }
}
