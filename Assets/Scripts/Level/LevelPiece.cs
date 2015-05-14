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
using System;

public class LevelPiece : MonoBehaviour
{
	// ========================================================================================\\

	private string id = "";
	public GameObject prefab;
	public float weight;
	public LevelPieceType pieceType;
	public Biome biome;
	private GameObject player;
    
	// ========================================================================================\\

	public LevelPiece (GameObject prefab, float chanceMultiplier)
	{
		this.prefab = prefab;
		this.weight = chanceMultiplier;
	}
    
	// ========================================================================================\\

	void Start ()
	{
		this.id = "GP_" + Guid.NewGuid ().ToString ("n");
	}

	void Update ()
	{
		if (player == null) {
			player = GameObject.Find ("Player");
		}

		if (player != null) {
			if (transform.position.x < player.transform.position.x - 15.0f) {
				Destroy (gameObject);
			}
		}
	}

	public GameObject GetPrefab ()
	{
		return prefab;
	}

	public float GetWeight ()
	{
		return weight;
	}

	public string GetID ()
	{
		return id;
	}

	public Biome GetBiome ()
	{
		return biome;
	}

	public LevelPieceType GetPieceType ()
	{
		return pieceType;
	}

	// ========================================================================================\\
}
