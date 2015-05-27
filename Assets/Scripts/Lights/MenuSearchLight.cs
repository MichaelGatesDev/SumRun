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

public class MenuSearchLight : MonoBehaviour
{
	// ========================================================================================\\

	public Transform startPos;
	public Transform endPos;
	public float speed = 1.0f;
	public float behindPadding = 10.0f;
	
	// ========================================================================================\\
	
	// Update is called once per frame
	void Update ()
	{
		// if positions are null, ignore
		if (startPos == null || endPos == null)
			return;

		// as long as the light hasn't reached the end
		if (transform.position.x < endPos.position.x) {
			// move it to the right
			transform.Translate (Vector3.right * Time.deltaTime * speed);
			// reached the end of the path
		} else {
			// reset position to beginning
			transform.position = new Vector3 (startPos.position.x - behindPadding, startPos.position.y, startPos.position.z);
		}
		
	}
	
	// ========================================================================================\\
}