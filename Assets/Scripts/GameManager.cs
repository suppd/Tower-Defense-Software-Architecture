using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	//variables
	public static bool GameIsOver;

	public GameObject gameOverUI;
	public GameObject completeLevelUI;

	//triggers
	public BaseTriggerScript triggerScript;

	void Awake()
	{
		GameIsOver = false;
		Debug.Log(PlayerInfo.Lives);
	}

	void Update()
	{
		if (GameIsOver)
			return;

		if (PlayerInfo.Lives <= 0)
		{
			EndGame();
		}
		if (triggerScript.enemyEntered)
        {
			PlayerInfo.Lives--;
			Debug.Log(PlayerInfo.Lives);
			triggerScript.enemyEntered = false;			
        }
	}


	void EndGame()
	{
		GameIsOver = true;
		gameOverUI.SetActive(true);
	}

	public void WinLevel()
	{
		GameIsOver = true;
		completeLevelUI.SetActive(true);
	}

}