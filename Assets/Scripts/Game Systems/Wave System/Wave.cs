using UnityEngine;

[System.Serializable]
public class Wave : MonoBehaviour
{

	/// <summary>
	/// this class is what you drag inside the waves[] in the wavespawner script in the gameManager basically its fully inspector configurable you drag an enemy or 2 in and then
	/// specify how many you want to spawn of both and at the rate so the interval between them
	/// </summary>

	public Monster enemy;
	public Monster enemy2;
	public int count;
	public float rate;

}