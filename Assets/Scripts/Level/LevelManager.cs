//------------------------------------------------------------------------------
//  Author:
//       Michael Gates <michaelgatesdev@gmail.com>
//
//  Copyright (c) 2015 Michael Gates 2015
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
	// ========================================================================================\\

	public GameObject playerPrefab;
	public GameObject spawnPosition;
	//
	private GameObject player;
	private GameObject deathBarrier;
	private Biome currentBiome;


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

	public void Restart ()
	{
		Application.LoadLevel ("Level");
	}

	public void AskQuit ()
	{
		GameObject.Find ("PauseGroup").GetComponent<Animator> ().SetBool ("paused", false);
		GameObject.Find ("AskGroup").GetComponent<Animator> ().SetBool ("asking", true);
	}

	public void QuitFromAsk ()
	{
		GameObject.Find ("AskGroup").GetComponent<Animator> ().SetBool ("quitting", true);
		
		Application.LoadLevel ("MainMenu");
	}

	public void CancelAsk ()
	{
		GameObject.Find ("AskGroup").GetComponent<Animator> ().SetBool ("asking", false);
		GetComponent<PauseManager> ().Unpause ();
	}

	// ========================================================================================\\
}
