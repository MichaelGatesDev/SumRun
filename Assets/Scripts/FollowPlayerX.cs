﻿//------------------------------------------------------------------------------
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

public class FollowPlayerX : MonoBehaviour
{
	// ========================================================================================\\

	private GameObject player;
	public int xOffset;
	
	// ========================================================================================\\

	// Use this for initialization
	void Start ()
	{
	}
    
	// Update is called once per frame
	void Update ()
	{
		if (player == null) {
			player = GameObject.Find ("Player");
		}

		float x = player.transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;
		 
		transform.position = Vector3.Lerp (transform.position, new Vector3(x + xOffset,y,z), 0.1f);
	}
	
	// ========================================================================================\\
}
