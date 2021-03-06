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

public class Fader : MonoBehaviour
{
	// ========================================================================================\\

	public Transform[] waypoints;
	private int currentWaypoint = 0;
	private Renderer ren;
	private bool fading = false;
	public float interval = 10.0f;
	public GameObject thunderLight;
	public GameObject spotLight;
	public ParticleSystem snowSystem;
	public ParticleSystem rainSystem;

	// ========================================================================================\\

	// Use this for initialization
	void Start ()
	{
		StartCoroutine ("SwitchBiome");
		ren = GetComponent<Renderer> ();
	}

	void Update ()
	{
		if (currentWaypoint == 1) {
			thunderLight.GetComponent<Light> ().enabled = true;
		} else {
			thunderLight.GetComponent<Light> ().enabled = false;
		}
        
		if (currentWaypoint != 2) {
			snowSystem.GetComponent<ParticleSystem> ().Pause ();
		} else {
			snowSystem.GetComponent<ParticleSystem> ().Play ();
		}
	}
    
	// ========================================================================================\\

	private IEnumerator SwitchBiome ()
	{
		while (true) {

			yield return new WaitForSeconds (interval);
            
			StartCoroutine ("FadeIn");
		}
	}
    
	private IEnumerator FadeOut ()
	{
		if (!fading) {
			fading = true;

			do {
				ren.material.color -= new Color (0, 0, 0, 0.025f);
                
				yield return new WaitForSeconds (0.01f);
			} while(ren.material.color.a > 0.0f);
            
			fading = false;
		}
        
		yield return null;
	}
    
	private IEnumerator FadeIn ()
	{
		if (!fading) {
			fading = true;

			do {
				ren.material.color += new Color (0, 0, 0, 0.025f);

				yield return new WaitForSeconds (0.01f);
			} while(ren.material.color.a < 1.0f);
            
			fading = false;

			// update waypoint
			currentWaypoint = (currentWaypoint >= waypoints.Length - 1 ? 0 : currentWaypoint + 1);
           
			// move main camera
			CameraUtility.MoveMain (waypoints [currentWaypoint].position);

			// move spotlight
			GameObject.Find ("SpotlightTracker").GetComponent<SpotlightTracker> ().Move (currentWaypoint);

		}

		yield return StartCoroutine ("FadeOut");
	}


	// ========================================================================================\\
}
