using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// Setting money text value for the UI
/// </summary>
public class SetMoneyDropText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;
    public void SetMoneyTextValue(int value)
    {
        moneyText.text = "+ "+value.ToString();
    }
}
