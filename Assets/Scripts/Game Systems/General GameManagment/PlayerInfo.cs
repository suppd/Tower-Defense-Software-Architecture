using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerInfo : MonoBehaviour
{
	/// <summary>
	/// this class is responsible of keeping track of all player related things like money health and rounds passed and also updating them and displaying them correctly!
	/// </summary>


	public static int Money;
	public int startMoney = 100;
	public static int Lives;
	public int startLives = 20;

	public static int Rounds;

	public static int upgradeCost = 5;
	public static int buildCost = 10;

	[SerializeField]
	private Canvas playerUI;
	[SerializeField]
	private TextMeshProUGUI moneyText;
	[SerializeField]
	private TextMeshProUGUI healthText;

    void Awake()
	{
		Money = startMoney;
		Lives = startLives;

		moneyText.text = Money.ToString();
		healthText.text = Lives.ToString();

		Rounds = 0;
	}
    private void FixedUpdate()
    {
		moneyText.text = Money.ToString();
		healthText.text = Lives.ToString();
	}

	public void SpendMoneyOnUpgrade()
    {
		if (Money >= upgradeCost)
		{
			Money = Money - upgradeCost;
		}
    }

	public void SpendMoneyOnBuild()
	{
		if (Money >= buildCost)
		{
			Money = Money - buildCost;
		}
	}

}