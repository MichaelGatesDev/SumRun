//------------------------------------------------------------------------------
//  ListWithDuplicates.cs
//
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

public class PlayerSlide : MonoBehaviour
{
	// =========================================================== \\
    
	private PlayerAnimate pa;
	private PlayerMove pm;
	private BoxCollider2D mainBC;
	private BoxCollider2D slideBC;
        
	// =========================================================== \\
    
	public void Start ()
	{
		pa = GetComponent<PlayerAnimate> ();
		pm = GetComponent<PlayerMove> ();
        
		mainBC = GameObject.Find ("player_collider_full").GetComponent<BoxCollider2D> ();
		slideBC = GameObject.Find ("player_collider_slide").GetComponent<BoxCollider2D> ();
        
		// player slide collision box does not need to be used yet
		slideBC.enabled = false;
	}
    
	// =========================================================== \\
    
	public void Slide ()
	{
		if (!pm.IsSliding ()) {
			pm.SetSliding (true);
			pa.ChangeState (PlayerAnimate.STATE_SLIDE); // run state
        
			// disable main collision
			mainBC.enabled = false;
        
			StartCoroutine ("FixCollider");
        
			// enable slide collision
			slideBC.enabled = true;
		}
	}
    
	private IEnumerator FixCollider ()
	{        
		yield return new WaitForSeconds (1.0f);
		pm.SetSliding (false);
		yield return new WaitForSeconds (0.3f);
		mainBC.enabled = true;
		slideBC.enabled = false;
        
        
		yield return null;
	}
    
	// =========================================================== \\
}

