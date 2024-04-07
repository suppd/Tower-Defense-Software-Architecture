using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    //this script's only purpose is enabling / disabling the upgrade button that is attached to the tower model (when the wave is active or not)\
    [SerializeField]
    private GameObject towerUpgradeButton;
    [SerializeField]
    private TowerBase towerScript;
    [SerializeField]
    private TextMeshProUGUI buttonText;
    public void SetButtonText()
    {
        buttonText.text = "upgrade -" + towerScript.TowerUpgradePrice;
    }
    private void Start()
    {
        SetButtonText();
    }
    private void FixedUpdate()
    {
        //enable upgrade button based on wheter its building phase or not
        if (GameManager.Instance != null)
        {
            if (WaveSpawner.Instance.WaveOver)
            {
                towerUpgradeButton.gameObject.SetActive(true);
            }
            else
            {
                towerUpgradeButton.gameObject.SetActive(false);
            }
        }
        // changing color based on if upgrade is possible
        if (PlayerInfo.Instance.GetPlayerMoneyAmount() < towerScript.TowerUpgradePrice)
        {
            towerUpgradeButton.GetComponent<Image>().color = Color.red;
            towerUpgradeButton.SetActive(false);
        }
        else if (WaveSpawner.Instance.WaveOver)
        {
            towerUpgradeButton.SetActive(true);
            towerUpgradeButton.GetComponent<Image>().color = Color.green;
        }
    }

}
