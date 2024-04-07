
public class NormalTower : TowerBase
{
    //Normal Tower Specific functions here
    protected override void UpgradeSpecifics()
    {
        Damage += DamageIncrease;
    }
}

