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

	private System.Random random;
	private bool addNewPieces = true; // hardcoded
	private int springCount = 0;
	private int winterCount = 0;
	private Biome forceBiome = Biome.SPRING;
	//
	private List<GameObject>spring_startPieces;
	private List<GameObject>spring_midPieces;
	private List<GameObject>spring_endPieces;
	private List<GameObject>spring_smallPieces;
	private List<GameObject>spring_xsmallPieces;
	private List<GameObject>winter_startPieces;
	private List<GameObject>winter_midPieces;
	private List<GameObject>winter_endPieces;
	private List<GameObject>winter_smallPieces;
	private List<GameObject>winter_xsmallPieces;
	public GameObject emptyPiece;
	//
	public GameObject lastGenerated;
	//
	public float x_spread = 6.0f;
	public float global_y;
	//
	public int max_spring = 20;
	public int max_winter = 20;
	//
	public GameObject[] prefabs;
	public GameObject apple;
	public GameObject goldenApple;
	public GameObject snowPrefab;

	// ========================================================================================\\

	public WorldGenerator ()
	{
		this.random = new System.Random ();
	}


	// Use this for initialization
	void Start ()
	{
		// setup the pieces
		StartCoroutine ("SetupPieces");

		// start adding pieces
		StartCoroutine ("AddPieces");

		// Spawn apples
		StartCoroutine ("SpawnApples");
	}

	// ========================================================================================\\
	/*
     * 
     * World Generators really suck...
     * 
     */
	// ========================================================================================\\
    
	private IEnumerator SetupPieces ()
	{
		yield return new WaitForSeconds (0.5f);

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


		foreach (GameObject go in prefabs) {
			if (go.GetComponent<LevelPiece> () == null)
				continue;
            
			LevelPiece goLP = go.GetComponent<LevelPiece> ();
            
			GameObject goPrefab = goLP.GetPrefab ();
            
			if (goPrefab == null)
				continue;
            
			float goWeight = goLP.GetWeight ();
            
			if (goWeight <= 0.0f) {
				goWeight = 0.1f;
			}

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



			/*
			for (int i=0; i<newWeight; i++) {
				if (goLP.biome == Biome.SPRING) {
					springPieces.Add (go);
				} else if (goLP.biome == Biome.WINTER) {
					winterPieces.Add (go);
				}
			}
			*/
		}

        
		yield return null;
	}

	private GameObject GetNextLikelyPiece ()
	{
		if (lastGenerated.GetComponent<LevelPiece> () == null) {
			return null;
		}

		LevelPieceType lastGeneratedType = lastGenerated.GetComponent<LevelPiece> ().pieceType;
		Biome lastBiome = lastGenerated.GetComponent<LevelPiece> ().biome;

		// if it generated quite a few spring pieces
		if (lastBiome == Biome.SPRING && springCount >= max_spring) {
			Debug.Log ("Checking if it's time for winter");
			int biomeChangeChance = random.Next (0, 100);

			// 25% chance to change biome
			if (biomeChangeChance >= 75) {
				forceBiome = Biome.WINTER;
				springCount = 0;
				winterCount = 0;
			}
		}
		// else if it generated quite a few winter pieces
		else if (lastBiome == Biome.WINTER && winterCount >= max_winter) {
			Debug.Log ("Checking if it's time for spring");
			int biomeChangeChance = random.Next (0, 100);
			
			// 25% chance to change biome
			if (biomeChangeChance >= 75) {
				forceBiome = Biome.SPRING;
				springCount = 0;
				winterCount = 0;
			}
		}


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
				foreach (GameObject go in winter_midPieces) {
					tempPieces.Add (go);
				}
				foreach (GameObject go in winter_endPieces) {
					tempPieces.Add (go);
				}
			}
			// if has to be spring
			else {
				foreach (GameObject go in spring_midPieces) {
					tempPieces.Add (go);
				}
				foreach (GameObject go in spring_endPieces) {
					tempPieces.Add (go);
				}
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
				foreach (GameObject go in winter_smallPieces) {
					tempPieces.Add (go);
				}
				foreach (GameObject go in winter_xsmallPieces) {
					tempPieces.Add (go);
				}
				foreach (GameObject go in winter_startPieces) {
					tempPieces.Add (go);
				}
			}
			// if has to be spring
			else {
				foreach (GameObject go in spring_smallPieces) {
					tempPieces.Add (go);
				}
				foreach (GameObject go in spring_xsmallPieces) {
					tempPieces.Add (go);
				}
				foreach (GameObject go in spring_startPieces) {
					tempPieces.Add (go);
				}
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
    
	private IEnumerator AddPieces ()
	{
		yield return new WaitForSeconds (1);

		while (addNewPieces) {
			AddNewPiece ();
			yield return new WaitForSeconds (0.95f);
		}
		yield return null;
	}

	private void AddNewPiece ()
	{
		if (lastGenerated == null) {
			Debug.LogError ("Houston, we have a problem.");
			return;
		}

		GameObject toAdd = GetNextLikelyPiece ();

		if (toAdd == null) {
			Debug.LogError ("MAJOR ISSUE! PIECE CAN'T GENERATE.");
			return;
		}

		Vector3 oldPos = lastGenerated.transform.position;
		float x = oldPos.x;
		float y = global_y;
		float z = oldPos.z;


		LevelPiece lp = toAdd.GetComponent<LevelPiece> ();

		if (lp.pieceType == LevelPieceType.EXTRA_SMALL) {
			y += 2.0f;
			x -= 1.0f;
		}
		else if(lp.pieceType == LevelPieceType.SMALL)
		{
			y += 2.0f;
		}

		if (lp.GetBiome () == Biome.SPRING) {
			springCount ++;
		} else if (lp.GetBiome () == Biome.WINTER) {
			winterCount++;
		}
		
		float z_spread = (float)random.NextDouble ();

		Vector3 newPos = new Vector3 (x + x_spread, y, z + z_spread);

		GameObject go = (GameObject)GameObject.Instantiate (toAdd, oldPos, Quaternion.identity);
		go.transform.position = newPos;

		lastGenerated = go;
	}

    
	// ========================================================================================\\

	private IEnumerator SpawnApples ()
	{
		yield return new WaitForSeconds (1);
        
		while (addNewPieces) {
			AddNewApple ();
			yield return new WaitForSeconds (1.0f);
		}

		yield return null;
	}

	private void AddNewApple ()
	{
		// if someone forgot to select the apple prefabs
		if (apple == null || goldenApple == null)
			return;

		bool shouldSpawn = random.Next (0, 100) >= 50;

		GameObject player = GameObject.Find ("Player");

		// if can spawn apple
		if (shouldSpawn) {

			Vector3 position = new Vector3 (player.transform.position.x + 25.0f, global_y + 4.5f, player.transform.position.z);
            
			bool shouldSpawnG = random.Next (0, 100) >= 80;

			if (shouldSpawnG) {
				Instantiate (goldenApple, position, Quaternion.identity);
			} else {
				Instantiate (apple, position, Quaternion.identity);
			}
		}

	}
	// ========================================================================================\\
}
