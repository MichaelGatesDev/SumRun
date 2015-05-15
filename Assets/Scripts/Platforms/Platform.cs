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

public class Platform : MonoBehaviour
{
	// =========================================================== \\
    
	private GameObject player;                  // the player
	private GameObject gm;
	private LevelManager lm;

	// =========================================================== \\
    
	void Start ()
	{
		gm = GameObject.Find("GameManager");
		lm = gm.GetComponent<LevelManager>();
	}

	void Update ()
	{
		if (player == null) {
			player = GameObject.Find ("Player");
		}
	}
    
	// =========================================================== \\

	// when something enters the platform's collision box
	void OnCollisionEnter2D (Collision2D col)
	{
		// if the object that entered is tagged 'Player'
		if (col.gameObject.tag == "Player") {
			// set the player to grounded
			//player.GetComponent<PlayerMove> ().SetGrounded (true);
			player.GetComponent<TestPlayerAnimate> ().SetJumping (false);

			Biome biome = gameObject.transform.parent.gameObject.GetComponent<LevelPiece>().GetBiome();
			lm.SetBiome(biome);

			// set player to 'no longer jumping' status
			//StartCoroutine ("NotJumping");
		}
	}
    
	// =========================================================== \\
    
	// Coroutine-ready function to make the player stop jumping
	private IEnumerator NotJumping ()
	{
		// set the player to 'not jumping'
		//TODO:

		// return nothing
		yield return null;
	}
    
    
	// =========================================================== \\
}
