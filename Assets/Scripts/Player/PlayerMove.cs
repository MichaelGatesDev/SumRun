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

public class PlayerMove : MonoBehaviour
{
    // ========================================================================================\\
    
    private float runSpeed = 5.0f;              // the speed the player runs at
    private float jumpForce = 300.0f;           // the force the player jumps at
    private bool mobileRun = false;             // if the game is being run from mobile (vs unity)
    private bool alive;                         // if the player is alive
    private bool grounded = false;              // if the player is on the ground (They start in the air)
    private bool jumping = false;               // if the player is jumping
    private bool sliding = false;               // if the palyer is sliding
    // separator
    private PlayerRun _pr;                      // PlayerRun class
    private PlayerJump _pj;                     // PlayerJump class
    private PlayerSlide _ps;                    // PlayerSlide class
    
    // ========================================================================================\\
    
    // Use this for initialization
    void Start()
    {
        // set player to 'alive'
        alive = true;
        
        // get movement components
        _pr = GetComponent<PlayerRun> ();
        _pj = GetComponent<PlayerJump> ();
        _ps = GetComponent<PlayerSlide> ();
    }
    
    // Update is called once per frame
    void Update()
    {
        // Handle input
        HandleInput ();
        
        // temporary debug
        //Debug.LogFormat ("Jumping: {0}, Grounded: {1}, Sliding: {2}", jumping, grounded, sliding);
    }

    
    // =========================================================== \\
    
    // Handle input
    void HandleInput()
    {
        // if playing on mobile
        if (mobileRun)
        {
            // handle mobile input elsehow
            HandleMobileInput ();
            // break out of this crazy place
            return;
        }

        // if they press 'jump'
        if (Input.GetKeyUp (KeyCode.Space))
        {
            // call 'jump' function from PlayerJump class
            _pj.Jump ();
        }
        // if they press 'slide'
        else if (Input.GetKeyUp (KeyCode.LeftShift))
        {
            // call 'slide' function from PlayerSlide class
            _ps.Slide ();
        }
        // they didn't press anything, so keep running
        else
        {
            // call the 'run' function from the PlayerRun class
            _pr.Run ();
        }
    }

    // Handle input for mobile devices
    void HandleMobileInput()
    {
    }
    
    // =========================================================== \\
    
    
    // returns the run speed of the player
    public float GetRunSpeed()
    {
        return runSpeed;
    }

    // returns the jump force of the player
    public float GetJumpForce()
    {
        return jumpForce;
    }
    
    // returns tru7e if the player is alive
    public bool IsAlive()
    {
        return alive;
    }

    // returns true if the player is on the ground
    public bool IsGrounded()
    {
        return grounded;
    }

    // returns true if the player is jumping
    public bool IsJumping()
    {
        return jumping;
    }

    // returns true if the player is sliding
    public bool IsSliding()
    {
        return sliding;
    }
    
    // returns true if the game is running outside of the unity engine
    public bool IsMobileRun()
    {
        return mobileRun;
    }
    
    
    // =========================================================== \\
    
    
    // set run speed
    public void SetRunSpeed(float v)
    {
        this.runSpeed = v;
    }

    // set jump force
    public void SetJumpForce(float v)
    {
        this.jumpForce = v;
    }
    
    // set alive status
    public void SetAlive(bool b)
    {
        this.alive = b;
    }
    
    // set grounded status
    public void SetGrounded(bool b)
    {
        this.grounded = b;
    }
    
    // set jumping status
    public void SetJumping(bool b)
    {
        this.jumping = b;
    }
    
    // set sliding status
    public void SetSliding(bool b)
    {
        this.sliding = b;
    }
    
    // =========================================================== \\
}
