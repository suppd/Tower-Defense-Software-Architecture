using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public interface ITowerObserver //interface for in the towershooting script to "observe" tower changes per tower type
{
    void NotifyNormalTowerUpgrade(int newLevel, float newFireRate, int newDamage);
    void NotifyAOETowerUpgrade(int newLevel, float newFireRate, float newRadius);
    void NotifyDebuffTowerUpgrade(int newLevel, float newFireRate, float newDebuffDuration);
}

public abstract class TowerBase : MonoBehaviour
{
    private List<ITowerObserver> observers = new List<ITowerObserver>();
    [Header("Tower Refrences that need refrencing in the prefab")]
    [SerializeField]
    private TowerShooting towerShootScript;
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private TextMeshProUGUI levelDisplay;
    [Header("Tower Building System Related Valuse")]
    [SerializeField]
    private int towerLevel = 1;
    [SerializeField]
    private int towerPrice = 10;
    [SerializeField]
    private int towerUpgradePrice = 5;
    //Even if not all towers do damage atleast have the value in the base class so that if you later on want for example the debuff tower to also deal small amount of damage or DOT (damage over time)
    [Header("Tower Base Values")]
    [SerializeField]
    private int towerDamage = 0;
    [SerializeField]
    private int towerFireRate = 0;
    [SerializeField]
    private int towerSlowDuration = 0;
    [SerializeField]
    private float towerExplosionRadius = 0; // make sure to set to 0 if 
    //The values that are upgraded by the upgrade system make them private and accesible in editor so upgrades can be adjusted for each tower type
    [Header("Tower Upgrade Gain Values")]
    [SerializeField]
    private int towerLevelGain = 1;
    [SerializeField]
    private int towerFireRateGain = 1;
    private float previousTowerExplosionRadius;
    public int Damage
    {
        get { return towerDamage; }
        protected set { towerDamage = value; }
    }
    public float ExplosionRadius
    {
        get { return towerExplosionRadius; }
        protected set { towerExplosionRadius = value; }
    }
    public int SlowDuration
    {
        get { return towerSlowDuration; }
        protected set { towerSlowDuration = value; }
    }
    //for if i want to update the upgrade price or normal buy price throughout the game at some point
    public int TowerPrice
    {
        get { return towerPrice; }
        protected set { towerPrice = value; }
    }
    public int TowerUpgradePrice
    {
        get { return towerUpgradePrice; }
        protected set { towerUpgradePrice = value; }
    }
    private void Start()
    {
        SetLocalShootingScriptAndSubsribe();
        UpdateShootingScriptValues();
        levelDisplay.text = towerLevel.ToString();
    }
    //we attach upgrade tower to upgrade button on tower prefab
    public void UpgradeTower() //every tower should have fire rate increase and level increase  and such  then implent their own upgrade value or other functionality
    {
        //common upgrade functionality
        PlayerInfo.Instance.SpendMoney(TowerUpgradePrice);
        towerFireRate += towerFireRateGain;
        towerLevel += towerLevelGain;
        //specific upgrade functionality
        UpgradeSpecifics();
        UpdateShootingScriptValues();
    }
    protected abstract void UpgradeSpecifics(); // now implement in subclass and give subclass specifc functionality
    public virtual void SetLocalShootingScriptAndSubsribe()
    {
        towerShootScript = gameObject.GetComponentInChildren<TowerShooting>();
        if (towerShootScript != null)
        {
            this.Subscribe(towerShootScript);
        }
    }
    public virtual void UpdateShootingScriptValues() // give shooting script the values so that it can pass it to the 
    {
        towerShootScript.BulletDamage = towerDamage;
        towerShootScript.FireRate = towerFireRate;
        towerShootScript.SlowDuration = towerSlowDuration;
        towerShootScript.ExplosionRadius = towerExplosionRadius;
        levelDisplay.text = towerLevel.ToString();
    }
    public void Subscribe(ITowerObserver observer)
    {
        observers.Add(observer);
    }
    public void Unsubscribe(ITowerObserver observer)
    {
        observers.Remove(observer);
    }
    protected void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            if (this is NormalTower)
            {
                Debug.Log("notifying of normal tower upgrade with new damage value of : " + towerDamage);
                observer.NotifyNormalTowerUpgrade(towerLevel, towerFireRate, towerDamage);
            }
            else if (this is AOETower)
            {
                Debug.Log("notifying of AOE tower upgrade with new damage value of : " + towerDamage);
                observer.NotifyAOETowerUpgrade(towerLevel, towerFireRate, towerExplosionRadius);
            }
            else if (this is DebuffTower)
            {
                Debug.Log("notifying of normal tower upgrade with new damage value of : " + towerDamage);
                observer.NotifyDebuffTowerUpgrade(towerLevel, towerFireRate, towerSlowDuration);
            }
        }
    }
   
    public virtual void destoryTower() // for the destroy button
    {
        this.Unsubscribe(towerShootScript);//unsubscribe when destroying
        Destroy(towerPrefab);
    }
}
