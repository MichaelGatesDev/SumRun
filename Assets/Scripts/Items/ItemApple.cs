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
using System.IO;   

public class ItemApple : MonoBehaviour
{
	// ========================================================================================\\

	public AppleType type;				// the type of apple
	public ParticleSystem effect;		// the effect to play (if one exists)
	private Player player;				// the player
	private GameObject go;				// the effect spawned-in
	
	// ========================================================================================\\

	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find ("Player").GetComponent<Player> ();

		// start particles
		StartParticles ();
	}

	void Update ()
	{
		if (transform.position.x < player.transform.position.x - 15.0f) {
			if (go != null) {
				Destroy (go);
			}
			Destroy (gameObject);
		}
	}

	// when a player touches it
	void OnTriggerEnter2D (Collider2D entered)
	{
		// if fairly normal apple types
		if (type == AppleType.NORMAL || type == AppleType.GOLD) {
			// add apples to player
			player.AddApples (1 * (type == AppleType.GOLD ? 5 : 1));
		}
		// if rotten apple
		else if (type == AppleType.ROTTEN) {
			// remove half of player's apples
			player.RemoveApples (player.GetApples () / 2);
			// poison the player
			player.Poison ();
		}
		// if poison apple
		else if (type == AppleType.POISON) {
			// kill the player because it's a poisonous apple
			player.Kill (PlayerDeathCause.POISON);
		}
		
		// destroy particle effect
		Destroy (go);
		// destroy apple object
		Destroy (gameObject);
	}

	// ========================================================================================\\
	
	private void StartParticles ()
	{
		// if apple does not have an effect, ignore
		if (effect == null)
			return;

		// create effect
		Object o = Instantiate (effect, transform.position, Quaternion.identity);
		o.name = "temp";
		go = GameObject.Find ("temp");
		go.name = RandomStr();	
	}
	
	// ========================================================================================\\

	public static string RandomStr()
		
	{
		string rStr = Path.GetRandomFileName();
		rStr = rStr.Replace(".", ""); // For Removing the .
		return rStr;
	}

}
