using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// this class is what you drag inside the Waves[] in the wavespawner script in the gameManager basically its fully inspector configurable you drag an enemy or 2 in and then
/// specify how many you want to spawn of both and at the rate so the interval between them
/// </summary>
[System.Serializable]
public class Wave : MonoBehaviour
{
	public List<Monster> Monsters = new List<Monster>();
	[Header("Enemies per wave per monster in the list")]
	public int Count;
	[Header("Rate Per Second, So 5 = 5 second")]
	public float Rate;
}