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

	public GameObject gameOverUI;
	public GameObject completeLevelUI;
	public GameObject gameplayUI;

    private float fixedDeltaTime;
    private void OnEnable() // subscribe to eventbus
    {
        GlobalBus.globalEventBus.Subscribe<BaseEnterEvent>(HandleEnemyEnter);
    }
    private void OnDisable() //unsubscribe to eventbus
    {
        GlobalBus.globalEventBus.UnSubscribe<BaseEnterEvent>(HandleEnemyEnter);
    }
    void Awake()
	{
        this.fixedDeltaTime = Time.fixedDeltaTime;
        GameIsOver = false;
		Debug.Log(PlayerInfo.Lives);
	}

	void Update()
	{
		if (PlayerInfo.Lives <= 0)
		{
			LoseLevel();
			WaveSpawner.Instance.enabled = false;
		}
	}
	//cheats for ingame
	public void AddMoney()
	{  
        PlayerInfo.Money+=1000;
    }
    public void AddTime(float timeToAdd)
	{
		Time.timeScale += timeToAdd;
		Debug.Log(Time.timeScale);
	}
    public void SubstractTime(float timeToSubstract)
    {
		if (Time.timeScale > 0.01f)
		{
            Debug.Log(Time.timeScale);
            Time.timeScale -= timeToSubstract;
		}
    }

    private void LoseLevel()
	{
		GameIsOver = true;
		gameOverUI.SetActive(true);
		gameplayUI.SetActive(false);
	}

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
    private void HandleEnemyEnter(object sender, EventArgs eventArgs)
    {
        PlayerInfo.Lives--;
        Debug.Log(PlayerInfo.Lives);
    }
}