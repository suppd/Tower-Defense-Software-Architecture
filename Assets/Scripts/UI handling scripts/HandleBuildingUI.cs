using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// this script makes build buttons red if player has insufficient money, sets the prices displayed on the buttons and sets the tower info panels text
/// it takes the lists from building manager script to do this at first I had preassigned lists in the editor/scene but to dynamically spawn UI its not possible to pre assign so I do it like this
/// </summary>
public class HandleBuildingUI : MonoBehaviour
{
    [SerializeField]
    private List<Button> buttons;
    [SerializeField] 
    private List<TextMeshProUGUI> buttonPriceTexts =new List<TextMeshProUGUI>();
    [SerializeField]
    private List<TextMeshProUGUI> buttonNameTexts =new List<TextMeshProUGUI>();
    private List<TowerScriptableObject> towerDatas;
    private List<GameObject> infoPanels;
    //NOTE : these lists need to be same order (index) for this script to function properly
    public void Update()
    {
        CheckButtonColor();
    }
    private void Start()
    {
        buttons = BuildingManagerScript.Instance.InstaniatedButtons;
        towerDatas = BuildingManagerScript.Instance.TowerDatas;
        infoPanels = BuildingManagerScript.Instance.InfoPanels;
        GetTextsFromButtons();
        SetButtonTexts();
        SetTowerInfoPanelTexts();
    }

    private void GetTextsFromButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttonPriceTexts[i] = buttons[i].gameObject.transform.Find("PriceText").GetComponent<TextMeshProUGUI>();
            buttonNameTexts[i] = buttons[i].gameObject.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        }
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
    private void SetButtonTexts()
    {
        for (int i = 0; i < towerDatas.Count; i++)
        {
            //Cut off the last bit of text -> (TowerScriptableObject) to make UI look nicer
            string originalText = towerDatas[i].ToString();
            int indexOfOpeningParenthesis = originalText.IndexOf('(');
            string modifiedText = originalText.Substring(0, indexOfOpeningParenthesis).Trim();
            buttonNameTexts[i].text = modifiedText;
            buttonPriceTexts[i].text = towerDatas[i].TowerPrice.ToString();
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
