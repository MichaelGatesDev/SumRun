using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
	// ========================================================================================\\

	public GameObject playerPrefab;
	public GameObject spawnPosition;
	//
	private int score;
	private bool alive;
	private GameObject player;
	private GameObject deathBarrier;

	// ========================================================================================\\

	// Use this for initialization
	void Start ()
	{
		// spawn the player
		SpawnPlayer ();

		// locate death barrier
		deathBarrier = GameObject.Find ("DeathBarrier");
	}
    
	// Update is called once per frame
	void Update ()
	{
		// if death barrier is not null
		if (deathBarrier != null) {
			// move death barrier below player
			deathBarrier.transform.position = new Vector3 (player.transform.position.x, deathBarrier.transform.position.y, 0);
		}
	}
    
	// ========================================================================================\\

	// spawns the player into the game
	private void SpawnPlayer ()
	{
		if (playerPrefab != null) {
			Object o = Instantiate (playerPrefab, spawnPosition.transform.position, Quaternion.identity);
			o.name = "Player";

			player = GameObject.Find ("Player");
		}
	}

	// kills the player
	public void KillPlayer ()
	{
		Debug.Log ("death to ye!");
	}
    
	// ========================================================================================\\

	// adds specified amount to player's score (apples)
	public void AddScore (int n)
	{
		this.score += n;
	}

	// sets the player's alive status
	public void SetAlive (bool b)
	{
		this.alive = b;
	}
    
	// ========================================================================================\\

	// returns if the player is alive
	public bool IsAlive ()
	{
		return alive;
	}

	// returns the player's current score
	public int GetScore ()
	{
		return score;
	}


	// ========================================================================================\\
}
