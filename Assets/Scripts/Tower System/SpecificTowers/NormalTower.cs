using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTower : TowerBase
{
    //Normal Tower Specific functions here
    protected override void UpgradeSpecifics()
    {
        Damage += DamageIncrease;
    }
}

