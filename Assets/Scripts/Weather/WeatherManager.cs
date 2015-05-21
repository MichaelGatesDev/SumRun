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
