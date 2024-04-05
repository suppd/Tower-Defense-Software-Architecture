using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffTower : TowerBase
{
    //Debuff Tower Specific functions here
    protected override void UpgradeSpecifics()
    {
        SlowDuration += SlowDurationIncrease;
    }
}
