using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManagerScript : MonoBehaviour
{
    /// <summary>
    /// Building Manager script
    /// has diffrent prefabs that the player can place (right now its 3 diffrent ones)
    /// gives information to playerinfo class for the cost of each build
    /// changes the turret to build variable that the building node class can use
    /// </summary>
    public static BuildingManagerScript instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("more than one buildmanager!!!");
            return;
        }
        instance = this;
    }
    public GameObject standardTurretPrefab;
    public GameObject aoeTurretPrefab;
    public GameObject debuffTurretPrefab;
    private void Start()
    {
        turretToBuild = standardTurretPrefab;
    }
    private GameObject turretToBuild;

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void setTurretToStandard()
    {
        PlayerInfo.buildCost = 10;
        turretToBuild = standardTurretPrefab;
    }
    public void setTurretToAOE()
    {
        PlayerInfo.buildCost = 20;
        turretToBuild = aoeTurretPrefab;
    }
    public void setTurretToDebuff()
    {
        PlayerInfo.buildCost = 30;
        turretToBuild = debuffTurretPrefab;
    }
}
