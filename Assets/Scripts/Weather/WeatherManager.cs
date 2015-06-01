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

public class WeatherManager : MonoBehaviour
{
	// ========================================================================================\\

	public ParticleSystem snow;
	//
	private bool snowing;
	//
	private GameObject created;
	private GameObject target;
	

	// ========================================================================================\\
	
	void Update ()
	{
		if (snowing)
			FollowTarget ();
	}
	
	// ========================================================================================\\

	public void LetItSnow ()
	{
		if (snowing)
			return;

		snowing = true;

		target = GameObject.Find ("Player");

		Object o = Instantiate (snow, target.transform.position, Quaternion.identity);
		o.name = "Snow";
		created = GameObject.Find ("Snow");
	}

	public void StopTheSnow ()
	{
		if (!snowing)
			return;

		snowing = false;
		Destroy (created);
	}
	
	// ========================================================================================\\

	private void FollowTarget ()
	{
		if (!created)
			return;

		Vector3 targetPos = new Vector3 ();
		targetPos.x = target.transform.position.x + 8;

		if (created.transform.position.y < target.transform.position.y + 5) {
			targetPos.y = target.transform.position.y + 5;
		}

		targetPos.z = target.transform.position.z;

		created.transform.position = targetPos;
	}


	// ========================================================================================\\
}
