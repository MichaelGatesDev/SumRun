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

public class PlayerKiller : MonoBehaviour
{
	// ========================================================================================\\

	private LevelManager lm;
    
	// ========================================================================================\\

	void OnTriggerEnter2D (Collider2D coll)
	{
		// if object that entered is not the player
		if (coll.gameObject.name != "WalkCollider")
			return;

		// kill the player
		Player player = coll.transform.parent.GetComponent<Player> ();

		// if the player is alive
		if (player.IsAlive ()) {
			// kill them
			player.Kill (PlayerDeathCause.FALL);
		}
	}

	// ========================================================================================\\
}
