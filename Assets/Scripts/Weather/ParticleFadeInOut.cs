using UnityEngine;
using System.Collections;

public class ParticleFadeInOut : MonoBehaviour
{
	private ParticleSystem ps;
	bool fading = false;

	// Use this for initialization
	void Start ()
	{
		ps = GetComponent<ParticleSystem> ();
	}

	public void FadeIn ()
	{
		if (!fading) {
			fading = true;
			InvokeRepeating ("DoFadeIn", 0.1f, 0.01f);
		}
	}
	
	public void FadeOut ()
	{
		// ignore if the ParticleSystem is paused (already faded out)
		if(ps.isPaused)
			return;
		
		if (!fading) {
			fading = true;
			InvokeRepeating ("DoFadeOut", 0.1f, 0.01f);
		}
	}

	private void DoFadeIn ()
	{
		if (ps.isPaused || !ps.isPlaying || ps.isStopped) {
			ps.Play ();
		}

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
	}

	private void DoFadeOut ()
	{
		ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
		ps.GetParticles (particles);
		
		for (int i=0; i<particles.Length; i++) {
			Color c = particles [i].color;
			
			if (c.a == 0.0f) {
				fading = false;
				CancelInvoke ("DoFadeOut");
				ps.Pause ();
			}
			
			particles [i].color = new Color (c.r, c.g, c.b, c.a - 0.01f);
		}
		
		ps.SetParticles (particles, particles.Length);
	}


}
