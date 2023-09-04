using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableAndEnableTowerButton : MonoBehaviour
{
    //this script's only purpose is enabling / disabling the upgrade button that is attached to the tower model (when the wave is active or not)
    public GameObject TowerUpgradeButton;

    private GameObject gameManager; // use gamemanager to know wheter or not wave is ongoing or not

    private void Awake()
    {
        //TowerUpgradeButton.gameObject.SetActive(false);
        gameManager = GameObject.FindGameObjectWithTag("GameController");
    }

    private void FixedUpdate()
    {
        //enable upgrade button based on wheter its building phase or not
        if (gameManager != null)
        {
            if (WaveSpawner.Instance.waveOver)
            {
                //Debug.Log("enabling upggrade");
                TowerUpgradeButton.gameObject.SetActive(true);
            }
            else
            {
                //Debug.Log("disabling upggrade");
                TowerUpgradeButton.gameObject.SetActive(false);
            }
        }
        // changing color based on if upgrade is possible
        if (PlayerInfo.Money < PlayerInfo.upgradeCost)
        {
            TowerUpgradeButton.GetComponent<Image>().color = Color.red;
            TowerUpgradeButton.SetActive(false);
        }
        else if (WaveSpawner.Instance.waveOver)
        {
            TowerUpgradeButton.SetActive(true);
            TowerUpgradeButton.GetComponent<Image>().color = Color.green;
        }
    }
}
