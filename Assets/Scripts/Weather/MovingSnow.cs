using UnityEngine;
using System.Collections;

public class MovingSnow : MonoBehaviour
{

	private GameObject player;
	private GameObject gm;
	private LevelManager lm;

	// Use this for initialization
	void Start ()
	{
		gm = GameObject.Find ("GameManager");
		lm = gm.GetComponent<LevelManager>();
	
		StartCoroutine ("CheckState");
	}

	private IEnumerator CheckState ()
	{
		while (true) {
			yield return new WaitForSeconds (1.0f);

			
			// if it is winter
			if(lm.GetCurrentBiome() == Biome.WINTER) {
				// fade in snow
				GetComponent<ParticleFadeInOut>().FadeIn();
			}

			// if not winter
			else {
				// fade out snow
				GetComponent<ParticleFadeInOut>().FadeOut();
			}

		}
	}

}
