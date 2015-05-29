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
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
	// ========================================================================================\\

	public LayerMask whatIsGround;
	public ParticleSystem deathParticle;
	//
	private Vector3 spawnPos;
	//
	private bool alive = true;
	private int score = 0;
	private int apples = 0;
	private int lives = 1;
	private bool poisoned;
	private Location location;
	private int distance = 0;

	// ========================================================================================\\

	// Use this for initialization
	void Start ()
	{
		distance = 0;
		spawnPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		CheckLocation ();


		// set new distance
		distance = (int)Vector3.Distance (spawnPos, transform.position);
	}
	
	// ========================================================================================\\

	public void Kill (PlayerDeathCause cause)
	{
		// set player to dead
		alive = false;

		Object temp = Instantiate (deathParticle, transform.position, Quaternion.identity);
		Destroy (temp, 5.0f);


		// Show gameover screen
		DoGameover ();
	}

	public void Revive ()
	{

	}

	public void Poison ()
	{

	}
	
	// ========================================================================================\\

	private void CheckLocation ()
	{
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector3.down, 30.0f, ~whatIsGround.value); 

		if (hit.collider != null && hit.collider.tag != null) {

			if (hit.collider.tag == "Obstacle")
				return;

			if (hit.collider.tag == "Utils" || !hit.collider || !hit.collider.transform || !hit.collider.transform.parent || !hit.collider.transform.parent.GetComponent<LevelPiece> ())
				return;

			Biome biome = hit.collider.transform.parent.GetComponent<LevelPiece> ().GetBiome ();
			location = new Location (gameObject.transform, biome);
		}
	}

	private void DoGameover ()
	{
		Text lblDistance = GameObject.Find("LabelDistanceAmount").GetComponent<Text>();
		lblDistance.text = distance + " meters";
		Text lblApples = GameObject.Find("LabelApplesAmount").GetComponent<Text>();
		lblApples.text = apples + "";
		Text lblScore = GameObject.Find("LabelScoreAmount").GetComponent<Text>();
		lblScore.text = (apples * 1000) + (distance * 100) + "";

		GameObject.Find ("PanelEnd").GetComponent<Animator> ().SetBool ("gameover", true);
	}

	// ========================================================================================\\

	public void AddLives (int amt)
	{
		this.lives += amt;
	}

	public void RemoveLives (int amt)
	{
		this.lives -= amt;
	}

	public void AddApples (int amt)
	{
		this.apples += amt;
	}

	public void RemoveApples (int amt)
	{
		this.apples -= amt;
	}
	
	// ========================================================================================\\

	public void SetAlive (bool b)
	{
		this.alive = b;
	}

	public void SetScore (int n)
	{
		this.score = n;
	}

	public void SetApples (int n)
	{
		this.apples = n;
	}

	public void SetLives (int n)
	{
		this.lives = n;
	}

	public void SetPoisoned (bool b)
	{
		this.poisoned = b;
	} 


	// ========================================================================================\\


	public bool IsAlive ()
	{
		return alive;
	}

	public int GetScore ()
	{
		return score;
	}

	public int GetApples ()
	{
		return apples;
	}

	public int GetLives ()
	{
		return lives;
	}

	public bool IsPoisoned ()
	{
		return poisoned;
	}

	public Location GetLocation ()
	{
		return location;
	}

	public int GetDistance ()
	{
		return distance;
	}

	// ========================================================================================\\
}
