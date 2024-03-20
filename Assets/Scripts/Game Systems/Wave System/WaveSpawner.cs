using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : Singleton<WaveSpawner> //inherit from singleton class to make wavespaner a singelton class (so theres only one in the game!)
{
	// wavespawner class is responsible for knowing when to start a new wave when a wave is still in progress when the game is over (by winning) 
	// and for of course keeping track of the number of the wave its on and then spawning the enemies based on a script inputed in the inspector
	public static int EnemiesAlive = 0;
	//arrray of waves to spawn which can be dragged in through the inspector 
	[Header("Here we configure waves in the inspector just drag the wave.cs script and then drag the enemy prefabs you want!")]
	public Wave[] waves;
	[Header("Required Refrences")]
	[SerializeField]
	private Transform spawnPoint;
	[SerializeField]
	private WayPoints wayPoints; // public so that we can pass the waypoints
	//public so that game state can be relayed to game manager
	public bool waveOver = true;
	//for skip button
	private bool skipTime = false;
	[SerializeField]
	private float timeBetweenWaves = 60f;
	[SerializeField]
	private float initalCountdown = 5f;
	[Header("UI Required Refrences")]
	[SerializeField]
	private TextMeshProUGUI waveCountdownText;
	[SerializeField]
	private TextMeshProUGUI waveOverText;
	[SerializeField]
	private TextMeshProUGUI waveCounterText;
	[SerializeField]
    private GameObject buildingPanel;
	private int waveIndex = 0;
	private int waveCountForText = 1;
	//for unit test
	[SerializeField]
	private bool isUnitTest = false;
	[SerializeField]
	private WayPoints[] wayPointsTest;
	enum WaveState
    {
		Start,
		Ongoing,
		Over,
		GameOver,
		UnitTest
    }
	private WaveState currentWaveState = WaveState.Over;
	private void Start()
    {
		if (!isUnitTest)
		{
			waveCounterText.text = "30";
		}
        else
        {
			currentWaveState = WaveState.UnitTest;
        }
    }
    void FixedUpdate()
	{
		if (!isUnitTest) //if its normal game loop
		{
			switch (CheckWaveState())
			{
				case WaveState.Start:
					MutualWaveState();
					StartCoroutine(SpawnWave());
					initalCountdown = timeBetweenWaves;
					break;
				case WaveState.Ongoing:
					waveOver = false;
					MutualWaveState();
					//
					break;
				case WaveState.Over:
					waveOver = true;
					MutualWaveState();
					setUI(true);
					break;
				case WaveState.GameOver:
					GameManager.Instance.WinLevel();
					this.enabled = false;
					break;
				case WaveState.UnitTest:
					//dont do anything
					break;
			}
		}
	}

	void MutualWaveState()
    {
		EnemiesAlive = GameObject.FindGameObjectsWithTag("enemy").Length;
		initalCountdown -= Time.deltaTime;
		initalCountdown = Mathf.Clamp(initalCountdown, 0f, Mathf.Infinity);
		waveCountdownText.text = string.Format("{0:00.00}", initalCountdown);
	}
	private WaveState CheckWaveState()
    {
		if (EnemiesAlive > 0)
		{
			//waveOver = false;
			return WaveState.Ongoing;
		}
		if (initalCountdown <= 0f || skipTime == true)
		{
			return WaveState.Start;
        }
		if (waveIndex == waves.Length)
        {
			return WaveState.GameOver;
        }
		else
		{
			return WaveState.Over;
		}
    }

	IEnumerator SpawnWave()
	{
		//handle UI when wave starts
		setUI(false);
		//also set the skip to false so that it resets
		skipTime = false;
		//update the amount of rounds in the playerinfo instance
		PlayerInfo.Rounds++;
		//get the wavescript that matches the current wave's index (so waveindex = 1 it gets the second wavescript in the array)
		Wave wave = waves[waveIndex];
		EnemiesAlive = wave.Count;
		for (int i = 0; i < wave.Count; i++)
		{	
			for (int j = 0; j < wave.monsters.Count; j++)
			{
				SpawnEnemy(wave.monsters[j]);
				Debug.Log(wave.monsters.Count);
				yield return new WaitForSeconds(wave.Rate / 1f);
			}
		}	
		waveIndex++;
		waveCountForText++;
	}
	private void setUI(bool ValueToSet) 
	{
        buildingPanel.SetActive(ValueToSet);
        waveCountdownText.enabled = ValueToSet;
        waveOverText.enabled = ValueToSet;
        waveCounterText.text = waveCountForText.ToString();
    }
	private void SpawnEnemy(Monster enemy)
	{
		Monster monster = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
		monster.pathingScript.WayPoints = wayPoints;
	}
	//for the skip button functionality
	public void SetSkip(bool skip)
    {
		skipTime = skip;
    }

	/// <summary>
	/// UNIT TEST METHODS that I call on buttons within unit tests scenes
	/// </summary>
	/// <param name="waveNum"></param>
	public void SpawnTestWave(int waveNum)
	{
		StartCoroutine(TestWaveSpawn(waveNum));
	}	
	public IEnumerator TestWaveSpawn(int waveNum)
	{
		Wave wave = waves[waveNum];
		for (int i = 0; i < wave.Count; i++)
		{
			SpawnEnemy(wave.monsters[0]);
			yield return new WaitForSeconds(1f / wave.Rate);
		}
	}
	// FOR CANNON UNIT TEST SO I CAN SPAWN 3 WAVES IN 3 DIFFRENT LINES
	public void SpawnDiffrentWaypointTest(int wayPoint)
	{
        Wave wave = waves[3];
        SpawnMultipleWaypointEnemy(wave.monsters[0], wayPoint);
    }
    public void SpawnMultipleWaypointEnemy(Monster enemy, int wayPoint)
    {
        Monster monster = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        monster.pathingScript.WayPoints = wayPointsTest[wayPoint];
    }
}