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
	public static int Rounds;
	public static int BuildCost = 10; //default
	private int money;
	[SerializeField]
	private int startMoney = 100;
	private int lives;
	[SerializeField]
	private int startLives = 20;
	[SerializeField]
	private Canvas playerUI;
	[SerializeField]
	private TextMeshProUGUI moneyText;
	[SerializeField]
	private TextMeshProUGUI healthText;

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
	public void SpendMoney(int amountToSubstact)
	{
		money -= amountToSubstact;
	}
	private new void Awake()
	{
		money = startMoney;
		lives = startLives;
		moneyText.text = money.ToString();
		healthText.text = lives.ToString();
		Rounds = 0;
	}
    private void FixedUpdate()
    {
		moneyText.text = money.ToString();
		healthText.text = lives.ToString();
	}
}