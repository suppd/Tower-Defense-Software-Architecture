
/// <summary>
/// //interface for in the towershooting script to "observe" tower changes per tower type
/// </summary>
public interface ITowerObserver 
{
    void NotifyNormalTowerUpgrade( int newFireRate, int newDamage);
    void NotifyAoeTowerUpgrade( int newFireRate, float newRadius);
    void NotifyDebuffTowerUpgrade( int newFireRate, float newDebuffDuration);
}