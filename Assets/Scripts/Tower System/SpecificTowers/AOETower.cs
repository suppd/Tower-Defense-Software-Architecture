
public class AOETower : TowerBase
{
    //AOE Tower Specific functions here
    protected override void UpgradeSpecifics()
    {
        ExplosionRadius += ExplosionRadiusIncrease;
    }
}
