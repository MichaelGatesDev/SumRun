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
using System.Collections.Generic;

public class WorldGenerator : MonoBehaviour
{
	// ========================================================================================\\
	
	public GameObject[] prefabs;				// array of all platforms that can be spawned
	public GameObject[] obstacles;				// array of all obstacles that can be spawned
	public GameObject apple;					// apple prefab
	public GameObject goldenApple;				// golden apple prefab
	public GameObject poisonApple;				// poison apple (kills player)
	public GameObject rottenApple;				// rotten apple (poisons player)
	public float x_spread = 6.0f;				// x axis spread between platforms
	public float global_y;						// the global y position for platforms
	//
	public int max_spring = 20;					// the max amount of spring (biome) pieces allowed before winter
	public int max_winter = 20;					// the max amount of winter (biome) pieces allowed before spring
	//
	public GameObject emptyPiece;				// 'blank' piece prefab
	//
	public int obstacleSpawnRate;				// the percentage (%) chance for an obstacle to spawn
	public int appleSpawnRate;					// the percentage (%) chance for an apple to spawn
	public int poisonAppleChance;				// the precentage (%) chance to spawn a poison apple
	public int rottenAppleChance;				// the precentage (%) chance to spawn a rotten apple
	public int goldenAppleChance;				// the precentage (%) chance to spawn a golden apple
	public int appleChance;						// the precentage (%) chance to spawn a normal apple
	public int appleObstaclePadding;			// the padding between apples and obstacles
	public int obstaclePadding;					// the padding between obstacles
	//
	private Player player;						// the player!
	private System.Random random;				// The GOOD random random random random thing
	private bool addNewPieces = true; 			// if new pieces can be added
	private int springCount = 0;				// the amount of spring pieces so far
	private int winterCount = 0;				// the maount of winter pieces so far
	private Biome forceBiome = Biome.SPRING;	// the biome being forced to spawn
	//
	private List<GameObject>spring_startPieces;	// collection of all spring start pieces
	private List<GameObject>spring_midPieces;	// collection of all spring mid pieces
	private List<GameObject>spring_endPieces;	// collection of all spring end pieces
	private List<GameObject>spring_smallPieces;	// collection of all spring small pieces
	private List<GameObject>spring_xsmallPieces;// collection of all spring extra small pieces
	private List<GameObject>winter_startPieces;	// collection of all winter start pieces
	private List<GameObject>winter_midPieces;	// collection of all winter mid pieces
	private List<GameObject>winter_endPieces;	// collection of all winter end pieces
	private List<GameObject>winter_smallPieces;	// collection of all winter small pieces
	private List<GameObject>winter_xsmallPieces;// collection of all winter extra small pieces
	//
	public GameObject lastGenerated;			// the last generated piece
	private GameObject lastGeneratedObstacle;	// the last generated obstacle
	private AppleType lastAppleType;			// the last type of apple generated

	// ========================================================================================\\

	// Use this for initialization
	void Start ()
	{
		// declare random
		this.random = new System.Random ();

		// setup the pieces
		StartCoroutine ("SetupPieces");

		// start adding pieces
		StartCoroutine ("AddPieces");

		// start adding obstacles
		InvokeRepeating ("SpawnObstacle", 1.0f, 1.0f);

		// spawn apples
		InvokeRepeating ("SpawnApple", 1.0f, 1.0f);
	}

	void Update ()
	{
		// get the player if it doesn't exist 
		if (!player) {
			GameObject temp = GameObject.Find ("Player");
			if (!temp) {
				return;
			}
			player = temp.GetComponent<Player> ();
		}

		// if the player isn't alive, ignore
		if (player == null)
			return;

		// if player is not alive
		if (!player.IsAlive ()) {
			// stop generating pieces
			StopCoroutine ("AddPieces");
			// stop spawning apples
			CancelInvoke ("SpawnApple");
			// stop spawning obstacles
			CancelInvoke ("SpawnObstacle");
		}

	}

	// ========================================================================================\\
	/*
     * 
     * 							World Generators really suck...
     * 
     */
	// ========================================================================================\\
    
	private IEnumerator SetupPieces ()
	{
		// initialize empty Lists for all pieces
		spring_startPieces = new List<GameObject> ();
		spring_midPieces = new List<GameObject> ();
		spring_endPieces = new List<GameObject> ();
		spring_smallPieces = new List<GameObject> ();
		spring_xsmallPieces = new List<GameObject> ();
		winter_startPieces = new List<GameObject> ();
		winter_midPieces = new List<GameObject> ();
		winter_endPieces = new List<GameObject> ();
		winter_smallPieces = new List<GameObject> ();
		winter_xsmallPieces = new List<GameObject> ();


		// iterate over all prefabs
		foreach (GameObject go in prefabs) {

			// if it doesn't have LevelPiece script, it's not a LevelPiece (or configured incorrectly)
			if (go.GetComponent<LevelPiece> () == null)
				continue;
            
			LevelPiece goLP = go.GetComponent<LevelPiece> (); // the level piece
			GameObject goPrefab = goLP.GetPrefab (); // the prefab (actual in-game piece)
            
			// if the prefab is null, skip (configured incorrectly)
			if (goPrefab == null)
				continue;
            

			float goWeight = goLP.GetWeight ();
            
			// every piece can generate (otherwise, why is it there?)
			if (goWeight <= 0.0f) {
				goWeight = 0.1f;
			}

			// just multiply weight by 100 :P
			int newWeight = (int)(goWeight * 100);
            
			// begin pieces
			if (goLP.pieceType == LevelPieceType.BEGIN) { 
				if (goLP.biome == Biome.SPRING) { 
					for (int i=0; i<newWeight; i++) { 
						spring_startPieces.Add (goPrefab); 
					} 
				} else {
					for (int i=0; i<newWeight; i++) { 
						winter_startPieces.Add (goPrefab); 
					} 
				} 
			}

			// mid pieces
			if (goLP.pieceType == LevelPieceType.MID) { 
				if (goLP.biome == Biome.SPRING) { 
					for (int i=0; i<newWeight; i++) { 
						spring_midPieces.Add (goPrefab); 
					} 
				} else {
					for (int i=0; i<newWeight; i++) { 
						winter_midPieces.Add (goPrefab); 
					} 
				} 
			}

			// end pieces
			if (goLP.pieceType == LevelPieceType.END) { 
				if (goLP.biome == Biome.SPRING) { 
					for (int i=0; i<newWeight; i++) { 
						spring_endPieces.Add (goPrefab); 
					} 
				} else {
					for (int i=0; i<newWeight; i++) { 
						winter_endPieces.Add (goPrefab); 
					} 
				} 
			}

			// small pieces
			if (goLP.pieceType == LevelPieceType.SMALL) { 
				if (goLP.biome == Biome.SPRING) { 
					for (int i=0; i<newWeight; i++) { 
						spring_smallPieces.Add (goPrefab); 
					} 
				} else {
					for (int i=0; i<newWeight; i++) { 
						winter_smallPieces.Add (goPrefab); 
					} 
				} 
			}

			// extra small pieces
			if (goLP.pieceType == LevelPieceType.EXTRA_SMALL) { 
				if (goLP.biome == Biome.SPRING) { 
					for (int i=0; i<newWeight; i++) { 
						spring_xsmallPieces.Add (goPrefab); 
					} 
				} else {
					for (int i=0; i<newWeight; i++) { 
						winter_xsmallPieces.Add (goPrefab); 
					} 
				} 
			}
		}
        
		yield return null;
	}

	private GameObject GetNextLikelyPiece ()
	{
		// if last generated piece doesn't have LevelPiece script, can't generate next piece
		if (lastGenerated.GetComponent<LevelPiece> () == null) {
			return null;
		}

		LevelPieceType lastGeneratedType = lastGenerated.GetComponent<LevelPiece> ().pieceType; // type of last generated
		Biome lastBiome = lastGenerated.GetComponent<LevelPiece> ().biome; // biome of last generated

		// if it generated quite a few spring pieces
		if (lastBiome == Biome.SPRING && springCount >= max_spring) {
			int biomeChangeChance = random.Next (0, 100);

			// 15% chance to change biome
			if (biomeChangeChance >= 85) {
				forceBiome = Biome.WINTER;
				springCount = 0;
				winterCount = 0;
			}
		}
		// else if it generated quite a few winter pieces
		else if (lastBiome == Biome.WINTER && winterCount >= max_winter) {
			int biomeChangeChance = random.Next (0, 100);
			
			// 15% chance to change biome
			if (biomeChangeChance >= 85) {
				forceBiome = Biome.SPRING;
				springCount = 0;
				winterCount = 0;
			}
		}


		// selected GameObject to return, initialized
		GameObject selected = null;


		// if last generated was a beginning piece
		if (lastGeneratedType == LevelPieceType.BEGIN) {

			// has to be mid 

			// if has to be winter
			if (forceBiome == Biome.WINTER && lastBiome == Biome.WINTER) {
				int ranPiece = random.Next (0, winter_midPieces.Count);
				selected = winter_midPieces [ranPiece];
			}
			// if has to be spring
			else {
				int ranPiece = random.Next (0, spring_midPieces.Count);
				selected = spring_midPieces [ranPiece];
			}
		}


        // if last generated was an ending piece
		else if (lastGeneratedType == LevelPieceType.END) {
			// has to be empty/air
			selected = emptyPiece;
		}

        
        // if last generated was a middle piece
        else if (lastGeneratedType == LevelPieceType.MID) {

			// has to be mid or end

			List<GameObject> tempPieces = new List<GameObject> ();

			// if has to be winter
			if (lastBiome == Biome.WINTER) {
				tempPieces.AddRange (winter_midPieces);
				tempPieces.AddRange (winter_endPieces);
			}
			// if has to be spring
			else {
				tempPieces.AddRange (spring_midPieces);
				tempPieces.AddRange (spring_endPieces);
			}

			int ranPiece = random.Next (0, tempPieces.Count);
			selected = tempPieces [ranPiece];
		}

        
        // if last generated piece was empty
        else if (lastGeneratedType == LevelPieceType.EMPTY) {
			// has to be small, extra small, or begin

			List<GameObject> tempPieces = new List<GameObject> ();
			
			// if has to be winter
			if (forceBiome == Biome.WINTER) {
				tempPieces.AddRange (winter_smallPieces);
				tempPieces.AddRange (winter_xsmallPieces);
				tempPieces.AddRange (winter_startPieces);
			}
			// if has to be spring
			else {
				tempPieces.AddRange (spring_smallPieces);
				tempPieces.AddRange (spring_xsmallPieces);
				tempPieces.AddRange (spring_startPieces);
			}
			
			int ranPiece = random.Next (0, tempPieces.Count);
			selected = tempPieces [ranPiece];
		}
        
        
        // if last generated piece was a small island
        else if (lastGeneratedType == LevelPieceType.SMALL) {
			// has to be empty
			selected = emptyPiece;
		}
        
        
        // if last generated piece was an extra small island
        else if (lastGeneratedType == LevelPieceType.EXTRA_SMALL) {
			// has to be empty
			selected = emptyPiece;
		}


		return selected;
	}

	// add piece Coroutine method
	private IEnumerator AddPieces ()
	{
		yield return new WaitForSeconds (1);

		// as long as we can add new pieces
		while (addNewPieces) {
			// add new piece
			AddNewPiece ();
			// wait almost a second
			yield return new WaitForSeconds (0.95f);
		}
		yield return null;
	}

	// add new piece to game
	private void AddNewPiece ()
	{
		// if last generated piece is corrupted/nonexistant, can't generate next piece
		if (lastGenerated == null) {
			Debug.LogError ("Failed to generate: last generated piece is nonexistant.");
			return;
		}

		// grab next likely piece
		GameObject toAdd = GetNextLikelyPiece ();

		// if objcet to add is corrupted/null
		if (toAdd == null) {
			Debug.LogError ("Failed to generate: object to generate is nonexistant.");
			return;
		}


		Vector3 oldPos = lastGenerated.transform.position;
		float x = oldPos.x;
		float y = global_y;

		LevelPiece lp = toAdd.GetComponent<LevelPiece> ();

		// if it's an extra small piece, move it a little so it's actually possible to use
		if (lp.pieceType == LevelPieceType.EXTRA_SMALL) {
			y += 2.0f;
			x -= 1.0f;
		}
		// if it's a small piece, move it a ltitle so it's actually possible to use
		else if (lp.pieceType == LevelPieceType.SMALL) {
			y += 2.0f;
		}

		// if biome is spring
		if (lp.GetBiome () == Biome.SPRING) {
			// increment spring piece count
			springCount ++;
		}
		// if biome is winter
		else if (lp.GetBiome () == Biome.WINTER) {
			// increment winter piece count
			winterCount++;
		}

		// z_spread is a random value. 
		// z_spread is necessary to prevent terrain sprites from overlapping and flasshing (z-fighting)
		float z_spread = float.Parse (random.NextDouble () + "");

		Vector3 newPos = new Vector3 (x + x_spread, y, z_spread);

		// prevent platforms from overspawning
		if (Vector3.Distance (player.transform.position, newPos) > 75)
			return;

		// add in the new game object
		GameObject go = (GameObject)GameObject.Instantiate (toAdd, oldPos, Quaternion.identity);
		go.transform.position = newPos;

		lastGenerated = go;
	}

    
	// ========================================================================================\\

	private void SpawnObstacle ()
	{
		LevelPieceType type = lastGenerated.GetComponent<LevelPiece> ().GetPieceType ();

		// can only spawn if the last generated was a mid type
		if (type == LevelPieceType.EMPTY || type == LevelPieceType.SMALL || type == LevelPieceType.EXTRA_SMALL) {
			return;
		}

		if (random.Next (0, 100) <= obstacleSpawnRate) {
			Vector3 position = new Vector3 (lastGenerated.transform.position.x, global_y + 3.8f, lastGenerated.transform.position.z);
			GameObject obstacle = obstacles [random.Next (0, obstacles.Length - 1)];

			// if obstacle is somehow null, ignore
			if (obstacle == null)
				return;


			// if not the first obstacle being spawned
			if (lastGeneratedObstacle != null) {
				// if obstacle spawning too close to last one, it's unfair
				if (Vector3.Distance (position, lastGeneratedObstacle.transform.position) < obstaclePadding)
					return;
			}

			// spawn in object
			lastGeneratedObstacle = (GameObject)Instantiate (obstacle, position, Quaternion.identity);
		}
	}

	private void SpawnApple ()
	{
		// if someone (me?) forgot to set apple prefabs
		if (!apple || !goldenApple || !poisonApple || !rottenApple)
			return;

		// if should spawn the apple
		bool shouldSpawn = random.Next (0, 100) <= appleSpawnRate;
		
		GameObject player = GameObject.Find ("Player");
		
		// if can't spawn apple, ignore
		if (!shouldSpawn)
			return;
			
		// position to spawn apple
		Vector3 position = new Vector3 (lastGenerated.transform.position.x, global_y + 4.5f, player.transform.position.z);



		// can only spawn apple in mid pieces
		if (lastGenerated.GetComponent<LevelPiece> ().GetPieceType () != LevelPieceType.MID)
			return;


		// if last obstacle exists
		if (lastGeneratedObstacle != null) {
			// don't let apples spawn so close to obstacles
			if (Vector3.Distance (position, lastGeneratedObstacle.transform.position) < appleObstaclePadding)
				return;
		}

		// chance of apple spawn type
		int ran = random.Next (0, 100);

		// can not be zero
		if (ran == 0)
			return;

		// chance to spawn poison apple
		if (ran <= poisonAppleChance && lastAppleType != AppleType.POISON) {
			Instantiate (poisonApple, position, Quaternion.identity);
			lastAppleType = AppleType.POISON;
		}
		// chance to spawn rotten apple
		else if (ran <= rottenAppleChance && lastAppleType != AppleType.ROTTEN) {
			Instantiate (rottenApple, position, Quaternion.identity);
			lastAppleType = AppleType.ROTTEN;
		}
		// chance to spawn golden apple
		else if (ran <= goldenAppleChance && lastAppleType != AppleType.GOLD) {
			Instantiate (goldenApple, position, Quaternion.identity);
			lastAppleType = AppleType.GOLD;
			// chance to spawn normal apple
		} else if (ran <= appleChance) {
			Instantiate (apple, position, Quaternion.identity);
			lastAppleType = AppleType.NORMAL;
		}
	}

	// ========================================================================================\\
}
