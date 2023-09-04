using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingNode : MonoBehaviour
{
    /// <summary>
    /// handles the actual "building" of towers on their designated nodes
    /// uses the refrence to the game manager to "spend" the players money and then gets the turret to build (selected by the player by using the UI buttons)
    /// and places it if the player has enough money
    /// </summary>
    /// 
    public Color hoverColor;
    public bool canBuild;
    private GameObject turret;

    private MeshRenderer rend;
    private Color startColor;

    private GameObject gameManager;
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        rend = GetComponent<MeshRenderer>();
        startColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        if (WaveSpawner.Instance.waveOver)
        {
            if (turret != null)
            {
                Debug.Log("cant build turret ");
                return;
            }
            if (PlayerInfo.Money >= PlayerInfo.buildCost)
            {
                gameManager.GetComponent<PlayerInfo>().SpendMoneyOnBuild();
                GameObject turretToBuild = BuildingManagerScript.Instance.GetTurretToBuild();
                turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);
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
