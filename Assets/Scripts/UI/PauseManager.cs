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

public class PauseManager : MonoBehaviour
{
	// ========================================================================================\\

	private bool paused = false;
	private float normalTime;

	private GameObject panelPause;
	private Animator ppAnimator;
	
	// ========================================================================================\\

	// Use this for initialization
	void Start ()
	{
		panelPause = GameObject.Find("PauseGroup");
		ppAnimator = panelPause.GetComponent<Animator>();
	}
	
	// ========================================================================================\\

	public void Pause ()
	{
		// if already paused, ignore
		if(paused)
			return;
		paused = true;
		
		normalTime = Time.timeScale;
		Time.timeScale = 0.0f;

		ppAnimator.SetBool("paused", true);
		Debug.Log ("Pause");
	}

	public void Unpause ()
	{
		// if already unpaused, ignore
		if(!paused)
			return;
		paused = false;

		Time.timeScale = normalTime;

		ppAnimator.SetBool("paused", false);
		Debug.Log ("Unpause");
	}
	
	// ========================================================================================\\

	public bool IsPaused ()
	{
		return paused;
	}
	
	// ========================================================================================\\
}
