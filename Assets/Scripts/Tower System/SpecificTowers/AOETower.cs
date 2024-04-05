using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOETower : TowerBase
{
    //AOE Tower Specific functions here
    protected override void UpgradeSpecifics()
    {
        ExplosionRadius += ExplosionRadiusIncrease;
    }
}
