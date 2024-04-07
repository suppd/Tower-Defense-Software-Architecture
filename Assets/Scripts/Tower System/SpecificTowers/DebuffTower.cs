
public class DebuffTower : TowerBase
{
    //Debuff Tower Specific functions here
    protected override void UpgradeSpecifics()
    {
        SlowDuration += SlowDurationIncrease;
    }
}
