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
	private bool paused = false;
	private float normalTime;
	private Animator pma;
	private Animator ppa;
	public GameObject[] mainUI;
	private GameObject pausecanvasObj;
	private GameObject pausepanelObj;

	// Use this for initialization
	void Start ()
	{
		pausecanvasObj = GameObject.Find ("PauseCanvas");
		pausepanelObj = GameObject.Find ("PausePanel");

		// iff the pause canvas is not null (otherwise probably debugging)
		if (pausecanvasObj != null)
			pma = pausecanvasObj.GetComponent<Animator> ();
		// if the pause panel is not null (otherwise probably debugging)
		if (pausepanelObj != null)
			ppa = pausepanelObj.GetComponent<Animator> ();
	}
    
	public void Pause ()
	{
		StartCoroutine ("DoPause");
	}

	public void Unpause ()
	{
		StartCoroutine ("DoUnpause");
	}
    
	private IEnumerator DoPause ()
	{
		paused = true;
		normalTime = Time.timeScale;

		ppa.Play ("PausePanelFadeIn");
		pma.Play ("PauseMenuFall"); //play fade in animation

		yield return new WaitForSeconds (0.5f);

		foreach (GameObject go in mainUI) {
			go.SetActive (false);
		}

		Time.timeScale = 0.0f;
		yield break;
	}

	private IEnumerator DoUnpause ()
	{
		paused = false;
        
		Time.timeScale = normalTime;
		
		ppa.Play ("PausePanelFadeOut");
		pma.Play ("PauseMenuRise"); //play fade in animation
        
		yield return new WaitForSeconds (0.5f);
        
		foreach (GameObject go in mainUI) {
			go.SetActive (true);
		}

		yield break;
	}

	public bool IsPaused ()
	{
		return paused;
	}

}
