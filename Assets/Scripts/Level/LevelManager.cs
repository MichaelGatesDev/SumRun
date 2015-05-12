using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
	// ========================================================================================\\

	public GameObject playerPrefab;
	public GameObject spawnPosition;

	// ========================================================================================\\

	// Use this for initialization
	void Start ()
	{
		SpawnPlayer ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	// ========================================================================================\\

	private void SpawnPlayer ()
	{
		if (playerPrefab != null) {
			Object o = Instantiate (playerPrefab, spawnPosition.transform.position, Quaternion.identity);
			o.name = "Player";
		}
	}
	
	// ========================================================================================\\
}
