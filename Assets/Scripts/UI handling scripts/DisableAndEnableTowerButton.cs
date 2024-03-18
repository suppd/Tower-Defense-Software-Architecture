using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableAndEnableTowerButton : MonoBehaviour
{
    //this script's only purpose is enabling / disabling the upgrade button that is attached to the tower model (when the wave is active or not)
    public GameObject TowerUpgradeButton;

    private void Awake()
    {
        //TowerUpgradeButton.gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        //enable upgrade button based on wheter its building phase or not
        if (GameManager.Instance != null)
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
        if (PlayerInfo.Instance.GetPlayerMoneyAmount() < PlayerInfo.upgradeCost)
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
