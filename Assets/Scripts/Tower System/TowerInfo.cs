using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TowerInfo : MonoBehaviour
{
    /// <summary>
    /// OUTDATED!!!!!!!!!!!!!!!!!!!!!!!!!! (check TowerBase.cs)
    /// </summary>
    // this class is responsible for managing each towers own values and information like damage firerate and such
    [SerializeField]
    private TowerShooting towerShootingScipt;
    public GameObject tower;
    public TextMeshProUGUI levelDisplay;

    private int towerLevel = 1;
    //make these public so they can be accessed by the towershooting script so that script can actually apply these values
    public int towerPrice = 10;
    public int towerUpgradePrice = 5;
    public int towerDamage = 8;
    public int towerFireRate = 1;
    public int towerSlowDuration = 3;
    public float towerExplosionRadius = 1.5f;
    //The values that are upgraded by the upgrade system make them public and accesible in editor so upgrades can be adjusted for each tower type
    public int allTowerLevelGain = 1;
    public int allTowerFireRateGain = 1;
    //
    public int NormalTowerDamageGain = 1;
    public float AOETowerRadiusGain = 0.5f;
    public int DebuffTowerSlowDurationGain = 1;
    //
    private float previousTowerExplosionRadius;
    private void Start()
    {
        updateTowerShootingScriptValuesAndLevel();
        levelDisplay.text = towerLevel.ToString();
    }
    // THREE DIFFRENT METHODS FOR UPDATING EACH DIFFRENT TOWER PREFAB (standard - bullet one , area of effect - damage one , the debuff tower)
    //They all upgrade diffrent values but have 2 shared values that are upgraded each time (fire rate and level)
    public void LevelUpStandardTower()
    {
        if (PlayerInfo.Instance.GetPlayerMoneyAmount() >= towerUpgradePrice) // change this to 
        {
            //PlayerInfo.Instance.SpendMoneyOnUpgrade();
            this.towerLevel += allTowerLevelGain;
            this.towerDamage += NormalTowerDamageGain;
            this.towerFireRate += allTowerFireRateGain;
            Debug.Log("this tower has been upgraded to have " + towerDamage + "amounts of damage per shot!");
            updateTowerShootingScriptValuesAndLevel();
        }
    }
    public void LevelUpAOETower()
    {
        if (PlayerInfo.Instance.GetPlayerMoneyAmount() >= towerUpgradePrice)
        {
            //PlayerInfo.Instance.SpendMoneyOnUpgrade();
            this.towerLevel += allTowerLevelGain;
            previousTowerExplosionRadius = towerExplosionRadius;
            this.towerExplosionRadius = previousTowerExplosionRadius + AOETowerRadiusGain;
            this.towerFireRate += allTowerFireRateGain;
            Debug.Log("this tower has been upgraded to have " + towerExplosionRadius + "big of a radius !");
            updateTowerShootingScriptValuesAndLevel();
        }
    }
    public void LevelUpDebuffTower()
    {
        if (PlayerInfo.Instance.GetPlayerMoneyAmount() >= towerUpgradePrice)
        {
            //PlayerInfo.Instance.SpendMoneyOnUpgrade();
            this.towerLevel += allTowerLevelGain;
            this.towerSlowDuration += DebuffTowerSlowDurationGain;
            this.towerFireRate += allTowerFireRateGain;
            Debug.Log("this tower has been upgraded to have " + towerSlowDuration + "amounts of slowduration !");
            updateTowerShootingScriptValuesAndLevel();
        }
    }
    public int getTowerLevel()
    {
        return this.towerLevel;
    }
    public int getTowerDamage()
    {
        return this.towerDamage;
    }
    // Updating the new values the tower gets when theyre upgraded so that theyre used in the actual shooting script
    private void updateTowerShootingScriptValuesAndLevel()
    {
        towerShootingScipt.BulletDamage = towerDamage;
        towerShootingScipt.FireRate = towerFireRate;
        towerShootingScipt.SlowDuration = towerSlowDuration;
        towerShootingScipt.ExplosionRadius = towerExplosionRadius;

        levelDisplay.text = towerLevel.ToString();
    }
    public void destoryTower() // for the destroy button
    {
        Destroy(tower);
    }
}

