using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManagerScript : Singleton<BuildingManagerScript> //inherit from singelton class
{
    /// <summary>
    /// Building Manager script
    /// has diffrent prefabs that the player can place (right now its 3 diffrent ones)
    /// gives information to playerinfo class for the cost of each build
    /// changes the turret to build variable that the building node class can use
    /// </summary>

    public GameObject StandardTurretPrefab;
    public GameObject AoeTurretPrefab;
    public GameObject DebuffTurretPrefab;
    private void Start()
    {
        turretToBuild = StandardTurretPrefab;
    }
    private GameObject turretToBuild;
    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
    public void setTurretToStandard()
    {
        PlayerInfo.BuildCost = StandardTurretPrefab.GetComponent<TowerInfo>().towerPrice;
        turretToBuild = StandardTurretPrefab;
    }
    public void setTurretToAOE()
    {

        PlayerInfo.BuildCost = AoeTurretPrefab.GetComponent<TowerInfo>().towerPrice;
        turretToBuild = AoeTurretPrefab;
    }
    public void setTurretToDebuff()
    {
        PlayerInfo.BuildCost = DebuffTurretPrefab.GetComponent<TowerInfo>().towerPrice;
        turretToBuild = DebuffTurretPrefab;
    }
}
