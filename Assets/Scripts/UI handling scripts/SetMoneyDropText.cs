
using UnityEngine;
using TMPro;
/// <summary>
/// Setting money text value for money drop effect
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
