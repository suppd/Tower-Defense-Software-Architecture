using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Wave : MonoBehaviour
{
	/// <summary>
	/// this class is what you drag inside the waves[] in the wavespawner script in the gameManager basically its fully inspector configurable you drag an enemy or 2 in and then
	/// specify how many you want to spawn of both and at the rate so the interval between them
	/// </summary>
	public List<Monster> monsters = new List<Monster>();
	[Header("Enemies per wave per monster in the list")]
	public int count;
	[Header("Rate Per Second, So 5 = 5 second")]
	public float rate;
}