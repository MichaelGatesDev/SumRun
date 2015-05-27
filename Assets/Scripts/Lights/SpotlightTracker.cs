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

/* Used to follow deer on main menu */
public class SpotlightTracker : MonoBehaviour
{
	// ========================================================================================\\

	public float[] yWaypoints;
	public float startX;
	
	// ========================================================================================\\

	// move spotlight around deer npc
	public void Move (int waypoint)
	{
		float x = gameObject.transform.position.x;
		float z = gameObject.transform.position.z;
		Vector3 newPos = new Vector3 (x, yWaypoints [waypoint], z);
		gameObject.transform.position = newPos;
		GameObject.Find ("Deer NPC").transform.position = newPos;
	}
	
	// ========================================================================================\\
}
