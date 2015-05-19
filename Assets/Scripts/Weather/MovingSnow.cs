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

			// if not winter
			if (lm.GetCurrentBiome() != Biome.WINTER) {
				// hide object
				GetComponent<ParticleFadeInOut>().FadeOut();
			}
			// if it is winter
			else {
				// show object
				GetComponent<ParticleFadeInOut>().FadeIn();
				Debug.Log ("It's winter!");
			}

		}
	}

}
