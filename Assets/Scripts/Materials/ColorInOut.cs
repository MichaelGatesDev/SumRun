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

public class ColorInOut : MonoBehaviour
{
	public Color startColor;
	public Color endColor;
	public float duration;
	private SpriteRenderer ren;
	private bool entering = true;
	private float t = 0;

	// Use this for initialization
	void Start ()
	{
		ren = GetComponent<SpriteRenderer> ();
	}

	void Update ()
	{
		if (entering) {
			ren.material.color = Color.Lerp (startColor, endColor, t);

			if (t < 1) {
				t += Time.deltaTime / duration;
			}

			if (ren.material.color == endColor) {
				entering = false;
				t = 0;
			}
		} else {
			ren.material.color = Color.Lerp (endColor, startColor, t);
			
			if (t < 1) {
				t += Time.deltaTime / duration;
			}

			if (ren.material.color == startColor) {
				entering = true;
				t = 0;
			}
		}
	}

}