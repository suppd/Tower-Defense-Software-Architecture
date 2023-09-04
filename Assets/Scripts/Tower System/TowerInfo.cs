using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInfo : MonoBehaviour
{
    // this class is responsible for managing each towers own values and information like damage firerate and such
    public TowerShooting towerShootingScipt;
    public GameObject tower;

    private int towerLevel = 1;

    //make these public so they can be accessed by the towershooting script so that script can actually apply these values
    public int towerDamage = 8;
    public int towerFireRate = 1;
    public int towerSlowDuration = 3;
    public float towerExplosionRadius = 1.5f;

    //The values that are upgraded by the upgrade system make them public and accesible in editor so upgrades can be adjusted for each tower type
    public int allTowerLevelGain = 1;
    //
    public int normalTowerDamageGain = 1;
    public float AOETowerRadiusGain = 0.5f;
    public int DebuffTowerSlowDurationGain = 1;
    //
    private float previousTowerExplosionRadius;
    private GameObject gameManager; // get the gamemanager so we can "spend" the players money
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        updateTowerShootingScript();
    }

    // THREE DIFFRENT METHODS FOR UPDATING EACH DIFFRENT TOWER PREFAB (standard - bullet one , area of effect - damage one , the debuff tower)
    public void LevelUpStandardTower()
    {
        if (PlayerInfo.Money >= PlayerInfo.upgradeCost)
        {
            gameManager.GetComponent<PlayerInfo>().SpendMoneyOnUpgrade();
            this.towerLevel += allTowerLevelGain;
            this.towerDamage += normalTowerDamageGain;
            Debug.Log("this tower has been upgraded to have " + towerDamage + "amounts of damage per shot!");
            updateTowerShootingScript();
        }
    }
    public void LevelUpAOETower()
    {
        if (PlayerInfo.Money >= PlayerInfo.upgradeCost)
        {
            gameManager.GetComponent<PlayerInfo>().SpendMoneyOnUpgrade();
            this.towerLevel += allTowerLevelGain;
            previousTowerExplosionRadius = towerExplosionRadius;
            this.towerExplosionRadius = previousTowerExplosionRadius + AOETowerRadiusGain;
            Debug.Log("this tower has been upgraded to have " + towerExplosionRadius + "big of a radius !");
            updateTowerShootingScriptExplosionRadius();
        }
    }
    public void LevelUpDebuffTower()
    {
        if (PlayerInfo.Money >= PlayerInfo.upgradeCost)
        {
            gameManager.GetComponent<PlayerInfo>().SpendMoneyOnUpgrade();
            this.towerLevel += allTowerLevelGain;
            this.towerSlowDuration += DebuffTowerSlowDurationGain;
            Debug.Log("this tower has been upgraded to have " + towerSlowDuration + "amounts of slowduration !");
            updateTowerShootingScriptSlowDuration();
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
    private void updateTowerShootingScript()
    {
        towerShootingScipt.bulletDamage = towerDamage;
    }

    private void updateTowerShootingScriptSlowDuration()
    {
        towerShootingScipt.slowDuration = towerSlowDuration;
    }

    private void updateTowerShootingScriptExplosionRadius()
    {
        towerShootingScipt.explosionRadius = towerExplosionRadius;
    }
    public void destoryTower() // for the destroy button
    {
        Destroy(tower);
    }
}

