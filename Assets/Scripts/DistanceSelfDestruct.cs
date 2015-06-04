using UnityEngine;
using System.Collections;

public class DistanceSelfDestruct : MonoBehaviour
{
	// ========================================================================================\\

	public float maxDistance = 15.0f;

	// ========================================================================================\\
	
	void Update()
	{
		DestroyMyself();
	}

	// ========================================================================================\\
	
	private void DestroyMyself ()
	{
		GameObject player = GameObject.Find ("Player");
		
		if (transform.position.x < player.transform.position.x - maxDistance) {
			Destroy (gameObject);
		}
	}

	// ========================================================================================\\
}
