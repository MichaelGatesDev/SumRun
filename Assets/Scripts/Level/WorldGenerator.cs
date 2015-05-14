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
	//
	private List<GameObject>possiblePieces;
	private List<GameObject>springPieces;
	private List<GameObject>winterPieces;
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

        
		possiblePieces = new List<GameObject> ();

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
            
			for (int i=0; i<newWeight; i++) {
				if (goLP.biome == Biome.SPRING) {
					springPieces.Add (go);
				} else if (goLP.biome == Biome.WINTER) {
					winterPieces.Add (go);
				}
			}
			//possiblePieces.Add (go);
		}

        
		yield return null;
	}
    

	private GameObject GetNextLikelyPiece ()
	{
		//TODO: finish biome grab
		int ranNum = random.Next (0, possiblePieces.Count);

		GameObject selected = possiblePieces [ranNum];
		LevelPieceType nextPieceType = selected.GetComponent<LevelPiece> ().GetPieceType ();
		Biome nextPieceBiome = selected.GetComponent<LevelPiece> ().GetBiome ();

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
				if (nextPieceBiome != Biome.WINTER) {
					// cycle again
					GetNextLikelyPiece ();
				}
			}
		}
		// else if it generated quite a few winter pieces
		else if (lastBiome == Biome.WINTER && winterCount >= max_winter) {
			Debug.Log ("Checking if it's time for spring");
			int biomeChangeChance = random.Next (0, 100);
			
			// 25% chance to change biome
			if (biomeChangeChance >= 75) {
				if (nextPieceBiome != Biome.SPRING) {
					// cycle again
					GetNextLikelyPiece ();
				}
			}
		}

		// if last generated was a beginning piece
		if (lastGeneratedType == LevelPieceType.BEGIN) {
			if (nextPieceType != LevelPieceType.MID) {
				return GetNextLikelyPiece ();
			}
		}


        // if last generated was an ending piece
        else if (lastGeneratedType == LevelPieceType.END) {
			if (nextPieceType != LevelPieceType.EMPTY) {
				return GetNextLikelyPiece ();
			}
		}

        
        // if last generated was a middle piece
        else if (lastGeneratedType == LevelPieceType.MID) {
			if (nextPieceType == LevelPieceType.BEGIN || nextPieceType == LevelPieceType.EMPTY || nextPieceType == LevelPieceType.EXTRA_SMALL || nextPieceType == LevelPieceType.SMALL) {
				return GetNextLikelyPiece ();
			}
		}

        
        // if last generated piece was empty
        else if (lastGeneratedType == LevelPieceType.EMPTY) {
			if (nextPieceType != LevelPieceType.SMALL && nextPieceType != LevelPieceType.EXTRA_SMALL && nextPieceType != LevelPieceType.BEGIN) {
				return GetNextLikelyPiece ();
			}
		}
        
        
        // if last generated piece was a small island
        else if (lastGeneratedType == LevelPieceType.SMALL) {
			if (nextPieceType != LevelPieceType.EMPTY) {
				return GetNextLikelyPiece ();
			}
		}
        
        
        // if last generated piece was an extra small island
        else if (lastGeneratedType == LevelPieceType.EXTRA_SMALL) {
			if (nextPieceType != LevelPieceType.EMPTY) {
				return GetNextLikelyPiece ();
			}
		}


		return selected;
	}
    
	private IEnumerator AddPieces ()
	{
		yield return new WaitForSeconds (1);

		while (addNewPieces) {
			AddNewPiece ();
			yield return new WaitForSeconds (1.0f);
		}
		yield return null;
	}

	private void AddNewPiece ()
	{
		if (lastGenerated == null) {
			Debug.Log ("Houston, we have a problem.");
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

		if (lp.GetBiome () == Biome.SPRING) {
			winterCount = 0;
			springCount ++;
		} else if (lp.GetBiome () == Biome.WINTER) {
			springCount = 0;
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
