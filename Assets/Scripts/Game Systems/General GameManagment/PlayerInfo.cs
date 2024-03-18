using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerInfo : Singleton<PlayerInfo>
{
	/// <summary>
	/// this class is responsible of keeping track of all player related things like money health and rounds passed and also updating them and displaying them correctly!
	/// </summary>


	private int money;
	[SerializeField]
	private int startMoney = 100;
	private  int lives;
	[SerializeField]
	private int startLives = 20;

	public static int rounds;
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
		money = startMoney;
		lives = startLives;

		moneyText.text = money.ToString();
		healthText.text = lives.ToString();

		rounds = 0;
	}
    private void FixedUpdate()
    {
		moneyText.text = money.ToString();
		healthText.text = lives.ToString();
	}

	public void SpendMoneyOnUpgrade()
    {
		if (money >= upgradeCost)
		{
			money = money - upgradeCost;
		}
    }

	public void SpendMoneyOnBuild()
	{
		if (money >= buildCost)
		{
			money = money - buildCost;
		}
	}
	public int GetPlayerLivesAmount()
    {
		return lives;
    }
	public int GetPlayerMoneyAmount()
	{
		return money;
	}
	public void AddPlayerMoney(int amountToAdd)
    {
		money += amountToAdd;
    }
	public void SubstractPlayerLives(int amountToSubstact)
    {
		lives -= amountToSubstact;
    }
}