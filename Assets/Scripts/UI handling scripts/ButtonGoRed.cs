using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGoRed : MonoBehaviour
{
    //OBSOLETE! -> HandleBuildingUI.cs
    //script for making build buttons color red if you dont have enough money for the corresponding tower build!
    [SerializeField]
    private List<Button> buttons;
    [SerializeField]
    private List<TowerScriptableObject> towerDatas;
    //NOTE : these 2 lists need to be same order for this script to function properly index 0 for example button is normal tower button and towerdata is normal tower data!
    public void Update()
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
}
