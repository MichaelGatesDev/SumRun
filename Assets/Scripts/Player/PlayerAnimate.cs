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

public class PlayerAnimate : MonoBehaviour
{
    // ========================================================================================\\
    
    private Animator animator;                  // Player animator
    //      
    public const int STATE_RUN = 0;             // the RUN state constant
    public const int STATE_JUMP = 1;            // the JUMP state constant
    public const int STATE_SLIDE = 2;           // the SLIDE state constant
    // 
    public int _currentAnimationState = 0;      // player's current animation state
    
    
    // ========================================================================================\\
    
    // Use this for initialization
    void Start()
    {
        // grab animator component
        animator = gameObject.GetComponentInChildren<Animator> ();
    }
    
    
    
    // Update is called once per frame
    void Update()
    {
    }
    
    // ========================================================================================\\
    
    // change the state
    public void ChangeState(int toState)
    {
        // if the state to change to is the current state
        if (toState == _currentAnimationState)
        {
            // ignore
            return;
        }
        
        
        switch (toState)
        {
        // default -> running
            default:
                this.animator.SetInteger ("state", STATE_RUN);
                break;
        // current -> running
            case STATE_RUN:
                this.animator.SetInteger ("state", STATE_RUN);
                this._currentAnimationState = STATE_RUN;
                break;
        // current -> jumping
            case STATE_JUMP:
                this.animator.SetInteger ("state", STATE_JUMP);
                this._currentAnimationState = STATE_JUMP;   
                break;
        // current -> sliding
            case STATE_SLIDE:
                this.animator.SetInteger ("state", STATE_SLIDE);
                this._currentAnimationState = STATE_SLIDE;
                break;
        }
    }
    
    // ========================================================================================\\
}
