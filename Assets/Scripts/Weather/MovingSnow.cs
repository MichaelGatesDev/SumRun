using UnityEngine;
using System.Collections;

public class MovingSnow : MonoBehaviour
{

	private GameObject player;
	private GameObject gm;
	private WorldGenerator wg;

	// Use this for initialization
	void Start ()
	{
		gm = GameObject.Find ("GameManager");
		wg = gm.GetComponent<WorldGenerator> ();
	
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
			if (wg.getCurrentBiome () != Biome.WINTER) {
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
