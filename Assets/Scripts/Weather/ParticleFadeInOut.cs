﻿using UnityEngine;
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
