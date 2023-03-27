using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (gameManager != null)
        {
            if (gameManager.GetComponent<WaveSpawner>().waveOver)
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
    }
}
