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
	public Wave[] waves;

	public Transform spawnPoint;
	public WayPoints wayPoints;

	public bool waveOver = true;

	//for skip button
	private bool skipTime = false;

	[SerializeField]
	private float timeBetweenWaves = 60f;
	[SerializeField]
	private float initalCountdown = 5f;

	[SerializeField]
	private TextMeshProUGUI waveCountdownText;
	[SerializeField]
	private TextMeshProUGUI waveOverText;
	[SerializeField]
	private TextMeshProUGUI buildingPhaseText;
	[SerializeField]
	private TextMeshProUGUI waveCounterText;

	[SerializeField]
	private Button buildButton1;
	[SerializeField]
	private Button buildButton2;
	[SerializeField]
	private Button buildButton3;
	[SerializeField]
	private Button skipButton;

	private GameObject gameManager;

	//local variables 
	private int waveIndex = 0;
	private int waveCountForText = 1;
	private void Awake()
	{
		// get the gamemanager object!
		gameManager = GameObject.FindGameObjectWithTag("GameController");
	}
    private void Start()
    {
		waveCounterText.text = "1";
    }
    void FixedUpdate()
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
			// handle UI after wave has passed
			buildButton1.gameObject.SetActive(true);
			buildButton2.gameObject.SetActive(true);
			buildButton3.gameObject.SetActive(true);
			skipButton.gameObject.SetActive(true);
			waveCountdownText.enabled = true;
			waveOverText.enabled = true;
			buildingPhaseText.enabled = true;
			waveOver =true;
			waveCounterText.text = waveCountForText.ToString();
		}

		//win the game when waves have reached max amount + stop waves from spawning by disabling
		if (waveIndex == waves.Length)
		{
			gameManager.GetComponent<GameManager>().WinLevel();
			this.enabled = false;
		}
		//spawn waves after cooldown
		if (initalCountdown <= 0f || skipTime == true)
		{
			StartCoroutine(SpawnWave());
			initalCountdown = timeBetweenWaves;
			return;
		}

		//update coutdown timer and the text for it
		initalCountdown -= Time.deltaTime;
		initalCountdown = Mathf.Clamp(initalCountdown, 0f, Mathf.Infinity);
		waveCountdownText.text = string.Format("{0:00.00}", initalCountdown);
	}

	IEnumerator SpawnWave()
	{
		//handle UI when wave starts
		waveCountdownText.enabled = false;
		waveOverText.enabled = false;
		buildingPhaseText.enabled = false;
		buildButton1.gameObject.SetActive(false);
		buildButton3.gameObject.SetActive(false);
		buildButton2.gameObject.SetActive(false);
		skipButton.gameObject.SetActive(false);
		//also set the skip to false so that it resets
		skipTime = false;
		//update the amount of rounds in the playerinfo instance
		PlayerInfo.Rounds++;

		//get the wavescript that matches the current wave's index (so waveindex = 1 it gets the second wavescript in the array)
		Wave wave = waves[waveIndex];

		EnemiesAlive = wave.count;
		for (int i = 0; i < wave.count; i++)
		{
			//spawn the initial enemy gives my the wave script
			SpawnEnemy(wave.enemy);
			yield return new WaitForSeconds(1f / wave.rate);		
			if (wave.enemy2 != null) // if the wave script has a second enemy dragged in spawn these too!
            {
				SpawnEnemy(wave.enemy2);
            }
			yield return new WaitForSeconds(1f / wave.rate);
		}
		
		waveIndex++;
		waveCountForText++;
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
}