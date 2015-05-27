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

public class ParticleFadeInOut : MonoBehaviour
{
	// ========================================================================================\\

	private ParticleSystem ps;
	private bool fading = false;
	public int state;
	
	// ========================================================================================\\

	// Use this for initialization
	void Start ()
	{
		ps = GetComponent<ParticleSystem> ();
	}
	
	// ========================================================================================\\

	// Fade In 
	public void FadeIn ()
	{
		InvokeRepeating ("DoFadeIn", 0.1f, 0.01f);
	}

	// Fade Out
	public void FadeOut ()
	{
		// ignore if the ParticleSystem is paused (already faded out)
		if (ps.isPaused)
			return;
		
		if (!fading) {
			InvokeRepeating ("DoFadeOut", 0.1f, 0.01f);
		}
	}

	private void DoFadeIn ()
	{
		fading = true;

		ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
		ps.GetParticles (particles);
			
		for (int i=0; i<particles.Length; i++) {
			Color c = particles [i].color;
		
			if (c.a == 1.0f) {
				fading = false;
				CancelInvoke ("DoFadeIn");
			}

			particles [i].color = new Color (c.r, c.g, c.b, c.a + 0.01f);
		}
			
		ps.SetParticles (particles, particles.Length);
		state = 1;
	}

	private void DoFadeOut ()
	{
		fading = true;

		ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
		ps.GetParticles (particles);
		
		for (int i=0; i<particles.Length; i++) {
			Color c = particles [i].color;
			
			if (c.a == 0.0f) {
				fading = false;
				CancelInvoke ("DoFadeOut");
			}
			
			particles [i].color = new Color (c.r, c.g, c.b, c.a - 0.01f);
		}
		
		ps.SetParticles (particles, particles.Length);
		state = 0;
	}

	// ========================================================================================\\
}
