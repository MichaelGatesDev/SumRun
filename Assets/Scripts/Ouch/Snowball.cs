using UnityEngine;
using System.Collections;

public class Snowball : MonoBehaviour
{
	// ========================================================================================\\

	public ParticleSystem effect;
	public float speed = 5.0f;

	// ========================================================================================\\

	void Update()
	{
		transform.Translate(Vector3.left * Time.deltaTime * speed);
	}

	// ========================================================================================\\

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.name == "Player") {
	
			Player player = GameObject.Find ("Player").GetComponent<Player> ();

			if (!player.IsAlive ())
				return;

			player.Kill (PlayerDeathCause.SNOWBALL);

			Object o  = Instantiate(effect, transform.position, Quaternion.identity);
			o.name = "SnowballEffect";

			Destroy (gameObject);
		}
	}

	// ========================================================================================\\
}
