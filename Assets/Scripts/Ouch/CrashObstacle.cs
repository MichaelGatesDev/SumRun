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

public class CrashObstacle : MonoBehaviour
{
	// ========================================================================================\\
	
	void Start ()
	{
		InvokeRepeating ("DestroyMyself", 0.5f, 0.5f);
	}
	// ========================================================================================\\

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.name == "Player") {
			Player player = GameObject.Find ("Player").GetComponent<Player> ();
			if (!player.IsAlive ())
				return;

			player.Kill (PlayerDeathCause.CRASH);
		}
	}
	
	// ========================================================================================\\
	
	private void DestroyMyself ()
	{
		GameObject player = GameObject.Find ("Player");
		
		if (transform.position.x < player.transform.position.x - 15.0f) {
			Destroy (gameObject);
		}
	}
	
	// ========================================================================================\\
}
