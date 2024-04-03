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
    //Changed all of these to the TowerBase Type to improve error handling and not let gameobjects without the script be refrenced
    [SerializeField]
    private TowerBase standardTurretPrefab;
    [SerializeField]
    private TowerBase aoeTurretPrefab;
    [SerializeField]
    private TowerBase debuffTurretPrefab;
    private TowerBase turretToBuild;
    public TowerBase GetTurretToBuild()
    {
        return turretToBuild;
    }
    public void setTurretToStandard()
    {
        PlayerInfo.BuildCost = standardTurretPrefab.TowerPrice;
        turretToBuild = standardTurretPrefab;
    }
    public void setTurretToAOE()
    {
        PlayerInfo.BuildCost = aoeTurretPrefab.TowerPrice;
        turretToBuild = aoeTurretPrefab;
    }
    public void setTurretToDebuff()
    {
        PlayerInfo.BuildCost = debuffTurretPrefab.TowerPrice;
        turretToBuild = debuffTurretPrefab;
    }
    private void Start()
    {
        turretToBuild = standardTurretPrefab;
    }
}
