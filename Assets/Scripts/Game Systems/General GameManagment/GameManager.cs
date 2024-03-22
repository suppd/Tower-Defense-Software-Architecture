using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> //inherit from singelton class
{
	/// <summary>
	/// the global gamemanager 
	/// mainly just handles the game state ( win - lose - playing states)
	/// also handles player money and lives
	/// </summary>
	//variables
	public static bool GameIsOver;
	[SerializeField]
	private GameObject gameOverUI;
	[SerializeField]
	private GameObject completeLevelUI;
	[SerializeField]
	private GameObject gameplayUI;
    private float fixedDeltaTime;
	[SerializeField]
	public void WinLevel()
	{
		GameIsOver = true;
		completeLevelUI.SetActive(true);
		gameplayUI.SetActive(false);
	}
	public void ReloadScene()
	{
		string currentSceneName = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene(currentSceneName);
	}
	//cheats for ingame
	public void AddMoney()
	{
		PlayerInfo.Instance.AddPlayerMoney(1000);
	}
	public void AddTime(float timeToAdd)
	{
		Time.timeScale += timeToAdd;
		//Debug.Log(Time.timeScale);
	}
	public void SubstractTime(float timeToSubstract)
	{
		if (Time.timeScale > 0.01f)
		{
			//Debug.Log(Time.timeScale);
			Time.timeScale -= timeToSubstract;
		}
	}
	private void OnEnable() // subscribe to eventbus
    {
        GlobalBus.GlobalEventBus.Subscribe<BaseEnterEvent>(HandleEnemyEnter);
    }
    private void OnDisable() //unsubscribe to eventbus
    {
        GlobalBus.GlobalEventBus.UnSubscribe<BaseEnterEvent>(HandleEnemyEnter);
    }
    void Awake()
	{
        this.fixedDeltaTime = Time.fixedDeltaTime;
        GameIsOver = false;
	}

	void Update()
	{
		if (PlayerInfo.Instance.GetPlayerLivesAmount() <= 0)
		{
			LoseLevel();
			WaveSpawner.Instance.enabled = false;
		}
	}
    private void LoseLevel()
	{
		GameIsOver = true;
		gameOverUI.SetActive(true);
		gameplayUI.SetActive(false);
	}
    private void HandleEnemyEnter(object sender, EventArgs eventArgs)
    {
		PlayerInfo.Instance.SubstractPlayerLives(1);
    }
}