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
			StartCoroutine ("DoFadeIn");
			Debug.Log ("Fade in particles");
		}
	}

	private void DoFadeIn ()
	{
		ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
		ps.GetParticles (particles);
			
		for (int i=0; i<particles.Length; i++) {
			Color c = particles [i].color;
		
			if (c.a == 1.0f) {
				fading = false;
				CancelInvoke("DoFadeIn");
			}

			particles [i].color = new Color (c.r, c.g, c.b, c.a + 0.01f);
		}
			
		ps.SetParticles (particles, particles.Length);
	}

	public void FadeOut ()
	{
		if (!fading) {
			fading = true;
			InvokeRepeating ("DoFadeOut", 0.1f, 0.01f);
			Debug.Log ("Fade out particles");
		}
	}

	private void DoFadeOut ()
	{
		ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
		ps.GetParticles (particles);
		
		for (int i=0; i<particles.Length; i++) {
			Color c = particles [i].color;
			
			if (c.a == 0.0f) {
				fading = false;
				CancelInvoke("DoFadeOut");
			}
			
			particles [i].color = new Color (c.r, c.g, c.b, c.a - 0.01f);
		}
		
		ps.SetParticles (particles, particles.Length);
	}


}
