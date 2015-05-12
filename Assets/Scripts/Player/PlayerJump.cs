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

public class PlayerJump : MonoBehaviour
{
    // ========================================================================================\\
    
    private PlayerAnimate pa;           // PlayerAnimate class
    private PlayerMove pm;              // PlayerMove class
    private float jumpForce;            // force of the player's jump
    
    // ========================================================================================\\
    
    public void Start()
    {
        // grab PlayerMove component
        pm = GetComponent<PlayerMove> ();
        // grab jump force from PlayerMove component
        jumpForce = pm.GetJumpForce ();
        // grab PlayerAnimate component
        pa = GetComponent<PlayerAnimate> ();
    }
    
    // ========================================================================================\\
    
    // make the player jump
    public void Jump()
    {
        // if the player is not already jumping AND the player is grounded
        if (!pm.IsJumping () && pm.IsGrounded ())
        {
            // set player to jumping
            pm.SetJumping (true);
            // change animation state to jumping
            pa.ChangeState (PlayerAnimate.STATE_JUMP);
            // move the player up by adding force to their rigidbody
            GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpForce);
        }
    }
    
    // ========================================================================================\\
}
