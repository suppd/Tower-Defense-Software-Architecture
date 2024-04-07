using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// handles the actual "building" of towers on their designated nodes
/// uses the refrence to the game manager to "spend" the players money and then gets the turret to build (selected by the player by using the UI buttons)
/// and places it if the player has enough money
/// </summary>
/// 
public class BuildingNode : MonoBehaviour
{
    [SerializeField]
    private Color hoverColor;
    private TowerBase turret;
    private MeshRenderer rend;
    private Color startColor;

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
        startColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        if (WaveSpawner.Instance.WaveOver)
        {
            if (turret != null)
            {
                Debug.Log("cant build turret ");
                return;
            }
            if (PlayerInfo.Instance.GetPlayerMoneyAmount() >= PlayerInfo.BuildCost)
            {
                TowerBase turretToBuild = BuildingManagerScript.Instance.GetTurretToBuild();
                turret = (TowerBase)Instantiate(turretToBuild, transform.position, transform.rotation);
                PlayerInfo.Instance.SpendMoney(PlayerInfo.BuildCost);
            }
        }
    }
    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
