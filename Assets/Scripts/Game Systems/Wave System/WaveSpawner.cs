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
	public Transform spawnPoint;
	public WayPoints wayPoints;
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
	//local variables 
	private int waveIndex = 0;
	private int waveCountForText = 1;
	//for unit test
	[SerializeField]
	private bool isUnitTest = false;
	[SerializeField]
	private WayPoints[] wayPointsTest;

	private void Start()
    {
		if (!isUnitTest)
		{
			waveCounterText.text = "30";
		}
    }
    void FixedUpdate()
	{
		if (!isUnitTest) 
		{ 
			//constantly update how many enemies are alive (i know this is an expensive method to call every frame but its the best for now)
			EnemiesAlive = GameObject.FindGameObjectsWithTag("enemy").Length;
			//Debug.Log("enemies alive right now : " + EnemiesAlive);
			//acting based on enemiesalive variable
			if (EnemiesAlive > 0)
			{
				waveOver = false;
				return;
			}
			else
			{
				waveOver = true;
				// handle UI after wave has passed
				setUI(true);
			}
				//win the game when waves have reached max amount + stop waves from spawning by disabling
				if (waveIndex == waves.Length)
				{
					GameManager.Instance.WinLevel();
					this.enabled = false;
				}
				//spawn waves after cooldown
				if (initalCountdown <= 0f || skipTime == true)
				{
					StartCoroutine(SpawnWave());
					initalCountdown = timeBetweenWaves;
					return;
				}
				//update countdown timer and the text for it
				initalCountdown -= Time.deltaTime;
				initalCountdown = Mathf.Clamp(initalCountdown, 0f, Mathf.Infinity);
				waveCountdownText.text = string.Format("{0:00.00}", initalCountdown);
			}
	}

	IEnumerator SpawnWave()
	{
		//handle UI when wave starts
		setUI(false);
		//also set the skip to false so that it resets
		skipTime = false;
		//update the amount of rounds in the playerinfo instance
		PlayerInfo.rounds++;
		//get the wavescript that matches the current wave's index (so waveindex = 1 it gets the second wavescript in the array)
		Wave wave = waves[waveIndex];
		EnemiesAlive = wave.count;
		for (int i = 0; i < wave.count; i++)
		{	
			for (int j = 0; j < wave.monsters.Count; j++)
			{
				SpawnEnemy(wave.monsters[j]);
				Debug.Log(wave.monsters.Count);
				yield return new WaitForSeconds(wave.rate / 1f);
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
		monster.pathingScript.wayPoints = wayPoints;
	}
	//for the skip button functionality
	public void SetSkip(bool skip)
    {
		skipTime = skip;
    }

	public void SpawnTestWave(int waveNum)
	{
		StartCoroutine(TestWaveSpawn(waveNum));
	}
	
	public IEnumerator TestWaveSpawn(int waveNum)
	{
		//handle UI when wave starts
		//also set the skip to false so that it resets
		skipTime = false;
		//update the amount of rounds in the playerinfo instance
		PlayerInfo.rounds++;
		Debug.Log(PlayerInfo.rounds);
		//get the wavescript that matches the current wave's index (so waveindex = 1 it gets the second wavescript in the array)
		Wave wave = waves[waveNum];
		EnemiesAlive = wave.count;
		for (int i = 0; i < wave.count; i++)
		{		//spawn the initial enemy gives my the wave script
			SpawnEnemy(wave.monsters[0]);
			yield return new WaitForSeconds(1f / wave.rate);
		}
	}
	// FOR CANNON UNIT TEST SO I CAN SPAWN 3 WAVES IN 3 DIFFRENT LINES
	public void SpawnDiffrentWaypointTest(int wayPoint)
	{
        Wave wave = waves[3];
        SpawnMultipleWaypointEnemy(wave.monsters[1], wayPoint);
    }
    public void SpawnMultipleWaypointEnemy(Monster enemy, int wayPoint)
    {
        Monster monster = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        monster.pathingScript.wayPoints = wayPointsTest[wayPoint];
    }
}