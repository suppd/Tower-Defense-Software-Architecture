using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This Script is creatable in the unity editor and then configurable to configure each tower prefab to use these configured settings simply drag this onto the desired tower prefab
/// At first I just handled this in each tower prefab but this makes configuring each prefab faster and easier
/// Plus it opens up the possibility for UI or other components to also get tower data so for example now my tower buy button wont have a hard coded price tag in the text
/// </summary>
[CreateAssetMenu]
public class TowerScriptableObject : ScriptableObject
{
    [Header("Tower Building System Related Values")]
    public int TowerLevel = 1;
    public int TowerPrice = 10;
    public int TowerUpgradePrice = 5;
    [Header("Tower Base Values")]
    [SerializeField]
    public int TowerDamage = 0;
    public int TowerFireRate = 0;
    public int TowerSlowDuration = 0;
    public float TowerExplosionRadius = 0;
    [Header("Tower Upgrade Gain Values")]
    public int TowerLevelGain = 1;
    public int TowerFireRateGain = 1;
    //The reason I put these here and not keep them in a Tower Specific Class is because
    // I want all my variables to be in one place for now so they're easy to adjust at once instead of having to adjust them here and also in prefab
    // In a bigger project this approach would quickly become messy and would probably require a seperate "upgrade" component for the towers
    [Header("Tower Upgrade Specific Values")]
    public int TowerDamageIncrease = 0;
    public float TowerRadiusIncrease = 0f;
    public int TowerSlowIncrease = 0;
    [Header("Prefab")]
    public TowerBase towerBase;
}
