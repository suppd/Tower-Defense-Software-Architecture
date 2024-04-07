using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// This Class  is the Superclass for all towers it handles upgrading all the commmon features and has a overridable method SpecificUpgrade for each tower type to override on its own
/// it loads all the assigned variables from the TowerScriptableObjectScript and notifies (using ITowerObserver) the TowerShootingScript when values are upgraded so it can adjust its values
/// 
/// </summary>
public abstract class TowerBase : MonoBehaviour
{
    private readonly List<ITowerObserver> observers = new List<ITowerObserver>();

    [Header("Tower References that need referencing in the prefab")]
    [SerializeField]
    private TowerScriptableObject towerDataScript;
    [SerializeField]
    private TowerShooting towerShootScript;
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private TextMeshProUGUI levelDisplay;
    [Header("Tower Building System Related Values")]
    private int towerLevel = 1;
    private int towerPrice = 10;
    private int towerUpgradePrice = 5;
    //Even if not all towers do damage atleast have the value in the base class so that if you later on want for example the debuff tower to also deal small amount of damage or DOT (damage over time)
    [Header("Tower Base Values")]
    private int towerDamage = 0;
    private int towerFireRate = 0;
    private int towerSlowDuration = 0;
    private float towerExplosionRadius = 0; 
    //The values that are upgraded by the upgrade system make them private and accesible in editor so upgrades can be adjusted for each tower type
    [Header("Tower Upgrade Gain Values")]
    private int towerLevelGain = 1;
    private int towerFireRateGain = 1;
    [Header("Specific Upgrade Values")]
    private int towerDamageIncrease = 0;
    private float towerRadiusIncrease = 0f;
    private int towerSlowIncrease = 0;
    [Header("Tower Upgrade Color System")]
    [SerializeField]
    private MeshRenderer render;
    [SerializeField]
    private Gradient gradient;
    public int Damage
    {
        get => towerDamage;
        protected set => towerDamage = value;
    }
    public float ExplosionRadius
    {
        get => towerExplosionRadius;
        protected set => towerExplosionRadius = value;
    }
    public int SlowDuration
    {
        get => towerSlowDuration;
        protected set => towerSlowDuration = value;
    }
    /// <summary>
    /// These 3 Variables were originally local variables that were serialized in the editor in each individual prefab (in scripts that inherit TowerBase)
    /// but since i reworked the tower data to be in a scriptable object it made more sense for me to have it all in one place
    /// because if i kept these prefab dependant it would almost defeat the purpose of scriptable objects as data containers (for this project specifically, small scale)
    /// so i made them public variables which all of the towers have now but not necessarily use
    /// </summary>
    public int DamageIncrease
    {
        get => towerDamageIncrease;
        protected set => towerDamageIncrease = value;
    }
    public float ExplosionRadiusIncrease
    {
        get => towerRadiusIncrease;
        protected set => towerRadiusIncrease = value;
    }
    public int SlowDurationIncrease
    {
        get => towerSlowIncrease;
        protected set => towerSlowIncrease = value;
    }
    ///
    public int TowerPrice
    {
        get => towerPrice;
        protected set => towerPrice = value;
    }
    public int TowerUpgradePrice
    {
        get => towerUpgradePrice;
        protected set => towerUpgradePrice = value;
    }
    public TowerScriptableObject TowerDataScript
    {
        get => towerDataScript;
        protected set => towerDataScript = value;
    }
    public void UpgradeTower() //every tower should have fire rate increase and level increase  and such  then implent their own upgrade value or other functionality
    {
        //common upgrade functionality
        UpgradeCommonTower();
        //specific upgrade functionality
        UpgradeSpecifics();
        NotifyObservers();
    }

    protected virtual void SetLocalShootingScriptAndSubscribe()
    {
        if (towerShootScript != null)
        {
            this.Subscribe(towerShootScript);
        }
    }
    private void Subscribe(ITowerObserver observer)
    {
        observers.Add(observer);
    }
    private void Unsubscribe(ITowerObserver observer)
    {
        observers.Remove(observer);
    }
    public virtual void DestoryTower() // for the destroy button
    {
        this.Unsubscribe(towerShootScript);//unsubscribe when destroying
        Destroy(towerPrefab);
    }
    private void UpgradeCommonTower()
    {
        PlayerInfo.Instance.SpendMoney(TowerUpgradePrice);
        towerFireRate += towerFireRateGain;
        towerLevel += towerLevelGain;
        UpgradeTowerColorBasedOnGradient();
        levelDisplay.text = towerLevel.ToString();
    }
    private void UpgradeTowerColorBasedOnGradient()
    {
        float normalizedLevel = (float)(this.towerLevel - 1) / 10f; // Normalize tower level to range [0, 1]
        render.material.color = gradient.Evaluate(normalizedLevel);
    }
    private void LoadTowerData(TowerScriptableObject towerScriptableObject)
    {
        //Building System values
        towerLevel = towerScriptableObject.TowerLevel;
        towerPrice = towerScriptableObject.TowerPrice;
        towerUpgradePrice = towerScriptableObject.TowerUpgradePrice;
        //Base Tower Values
        Damage = towerScriptableObject.TowerDamage;
        towerFireRate = towerScriptableObject.TowerFireRate;
        towerSlowDuration = towerScriptableObject.TowerSlowDuration;
        towerExplosionRadius = towerScriptableObject.TowerExplosionRadius;
        //Tower Upgrade gain Values
        towerLevelGain = towerScriptableObject.TowerLevelGain;
        towerFireRateGain = towerScriptableObject.TowerFireRateGain;
        towerDamageIncrease = towerScriptableObject.TowerDamageIncrease;
        towerRadiusIncrease = towerScriptableObject.TowerRadiusIncrease;
        towerSlowIncrease = towerScriptableObject.TowerSlowIncrease;
    }
    private void Start()
    {
        LoadTowerData(towerDataScript);
        SetLocalShootingScriptAndSubscribe();
        NotifyObservers(); //Call it once so that the initial values can be set
        levelDisplay.text = towerLevel.ToString();
    }
    protected abstract void UpgradeSpecifics(); // now implement in subclass and give subclass specifc functionality
    private void NotifyObservers() //way of letting the actual turret that shoots the bullet prefabs know that a value has changed for the bullet 
    {
        foreach (var observer in observers)
        {
            switch (this)
            {
                case NormalTower _:
                    Debug.Log("notifying of normal tower upgrade with new Damage value of : "  + towerDamage);
                    observer.NotifyNormalTowerUpgrade(towerFireRate, towerDamage);
                    break;
                case AOETower _:
                    Debug.Log("notifying of normal tower upgrade with new ExplosionRadius value of : "  + towerExplosionRadius);
                    observer.NotifyAoeTowerUpgrade( towerFireRate, towerExplosionRadius);
                    break;
                case DebuffTower _:
                    Debug.Log("notifying of normal tower upgrade with new SlowDuration value of : "  + towerSlowDuration);
                    observer.NotifyDebuffTowerUpgrade(towerFireRate, towerSlowDuration);
                    break;
            }
        }
    }
}
