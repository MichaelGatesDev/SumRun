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

	// Update is called once per frame
	void Update ()
	{
		if (player == null) {
			GameObject tempPlayer = GameObject.Find ("Player");
			if (tempPlayer == null)
				return;
			player = tempPlayer;
		}

		float x = player.transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;

		transform.position = new Vector3 (x + 7.5f, y, z);
	}

	private IEnumerator CheckState ()
	{
		while (true) {
			yield return new WaitForSeconds (1.0f);

			// if not winter
			if (lm.GetCurrentBiome() != Biome.WINTER) {
				// hide object
				GetComponent<ParticleSystem> ().Stop ();
			}
			// if it is winter
			else {
				// show object
				GetComponent<ParticleSystem> ().Play ();
				Debug.Log ("It's winter!");
			}

		}
	}

}
