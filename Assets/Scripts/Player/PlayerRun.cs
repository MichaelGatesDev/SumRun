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

public class PlayerRun : MonoBehaviour
{
    // ========================================================================================\\
    
    private PlayerAnimate pa;                       // PlayerAnimate class
    private PlayerMove pm;                          // PlayerMove class
    private float runSpeed;                         // The speed at which the player runs
    
    // ========================================================================================\\
    
    public void Start()
    {
        // get the player's run speed from the PlayerMove class component
        runSpeed = GetComponent<PlayerMove> ().GetRunSpeed ();
        // grab PlayerAnimate component
        pa = GetComponent<PlayerAnimate> ();
        // grab PlayerMove component
        pm = GetComponent<PlayerMove> ();
    }
    
    // ========================================================================================\\
    
    // Make the player run
    public void Run()
    {
        // If the player is not currently jumping AND they are not currently sliding
        if (!pm.IsJumping () && !pm.IsSliding ())
        {
            pa.ChangeState (PlayerAnimate.STATE_RUN); // run state
        }
        
        gameObject.transform.Translate (Vector2.right * Time.deltaTime * runSpeed); // move the player
        
    }
    
    // ========================================================================================\\
}
