using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandleBuildingUI : MonoBehaviour
{
    //this script makes build buttons red if player has insuffiecient money, sets the prices displayed on the buttons and sets the tower info panels text
    [SerializeField]
    private List<Button> buttons;
    [SerializeField] 
    private List<TextMeshProUGUI> buttonTexts;
    [SerializeField]
    private List<TowerScriptableObject> towerDatas;
    [SerializeField]
    private List<GameObject> infoPanels;
    //NOTE : these lists need to be same order (index) for this script to function properly
    public void Update()
    {
        CheckButtonColor();
    }
    private void Start()
    {
        SetButtonPriceTexts();
        SetTowerInfoPanelTexts();
    }
    private void CheckButtonColor()
    {
        for (int i = 0; i < towerDatas.Count; i++)
        {
            if (PlayerInfo.Instance.GetPlayerMoneyAmount() < towerDatas[i].TowerPrice)
            {
                buttons[i].image.color = Color.red;
                buttons[i].interactable = false;
            }
            else
            {
                buttons[i].image.color = Color.white;
                buttons[i].interactable = true;
            }
        }
    }
    private void SetButtonPriceTexts()
    {
        for (int i = 0; i < towerDatas.Count; i++)
        {
            buttonTexts[i].text = towerDatas[i].TowerPrice.ToString();
        }
    }
    private void SetTowerInfoPanelTexts()
    {
        for (int i = 0; i < towerDatas.Count; i++)
        {
            GameObject infoPanel = infoPanels[i];
            TextMeshProUGUI[] textComponents = infoPanel.GetComponentsInChildren<TextMeshProUGUI>();
            string[] attributeNames = { "Damage", "FireRate", "Radius", "SlowDuration" };
            float[] attributeValues = { towerDatas[i].TowerDamage, towerDatas[i].TowerFireRate, towerDatas[i].TowerExplosionRadius, towerDatas[i].TowerSlowDuration };
            for (int j = 0; j < attributeNames.Length; j++)
            {
                textComponents[j].text = attributeNames[j] + " : " + attributeValues[j].ToString();
            }
        }
    }
}
