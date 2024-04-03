
public interface ITowerObserver //interface for in the towershooting script to "observe" tower changes per tower type
{
    void NotifyNormalTowerUpgrade(float newLevel, float newFireRate, int newDamage);
    void NotifyAoeTowerUpgrade(float newLevel, float newFireRate, float newRadius);
    void NotifyDebuffTowerUpgrade(float newLevel, float newFireRate, float newDebuffDuration);
}