using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
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
    private void OnEnable() // subscribe to eventbus
    {
        GlobalBus.sync.Subscribe<BaseEnterEvent>(HandleEnemyEnter);
    }
    private void OnDisable() //unsubscribe to eventbus
    {
        GlobalBus.sync.UnSubscribe<BaseEnterEvent>(HandleEnemyEnter);
    }
    void Awake()
	{
		GameIsOver = false;
		Debug.Log(PlayerInfo.Lives);
	}

	void Update()
	{
		if (GameIsOver)
		{
			gameOverUI.SetActive(true);
			//pause time or stop waves from spawning or smth..? atleast make it so that player cant continue playing
		}
		if (PlayerInfo.Lives <= 0)
		{
			LoseLevel();
			this.GetComponentInParent<WaveSpawner>().enabled = false;
		}
		//CHEATS
		if (Input.GetKey(KeyCode.P))
		{
			PlayerInfo.Money++;
		}
        else if (Input.GetKey(KeyCode.Minus))
        {
            PlayerInfo.Money--;
        }
    
	}

    private void LoseLevel()
	{
		GameIsOver = true;
		gameOverUI.SetActive(true);
	}

	public void WinLevel()
	{
		GameIsOver = true;
		completeLevelUI.SetActive(true);
	}
    private void HandleEnemyEnter(object sender, EventArgs eventArgs)
    {
        PlayerInfo.Lives--;
        Debug.Log(PlayerInfo.Lives);
    }
}