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

	private Player player;

	// ========================================================================================\\

	// Use this for initialization
	void Start ()
	{
		InvokeRepeating ("WeatherCheck", 0.1f, 0.1f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!player)
		{
			if(!GameObject.Find("Player"))
				return;

			player = GameObject.Find ("Player").GetComponent<Player> ();
		}
	}
	
	// ========================================================================================\\
	
	private void WeatherCheck ()
	{
		Biome biome = player.GetLocation ().GetBiome ();

		GameObject snow = GameObject.Find("Snow");
		
		// if snow doesn't exist.. !@#$%
		if(snow == null)
			return;
		
		ParticleFadeInOut particleFade = snow.GetComponent<ParticleFadeInOut>();


		if (biome == Biome.SPRING) {

			if(particleFade.state == 1)
			{
				particleFade.FadeOut();
			}

		} else if (biome == Biome.WINTER) {

			if(particleFade.state == 0)
			{
				particleFade.FadeIn();
			}

		}

	}
	
	// ========================================================================================\\
}
