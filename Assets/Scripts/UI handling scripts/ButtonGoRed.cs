using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGoRed : MonoBehaviour
{
    //script for makign build buttons color red if you dont have enough money for the corresponding tower build!
    [SerializeField]
    private List<Button> buttons;
    [SerializeField]
    private List<GameObject> prefabs;

    public void Update()
    {
        for (int i = 0; i < prefabs.Count; i++)
        {
            if (PlayerInfo.Instance.GetPlayerMoneyAmount() < prefabs[i].GetComponent<TowerBase>().TowerPrice)
            {
                buttons[i].image.color = Color.red;
            }
            else
            {
                buttons[i].image.color = Color.white;
            }
        }
    }
}
