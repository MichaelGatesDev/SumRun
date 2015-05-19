using UnityEngine;
using System.Collections;

public class ItemApple : MonoBehaviour
{
	// ========================================================================================\\

	public bool gold;
	private Player player;
    
	// ========================================================================================\\

	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find ("Player").GetComponent<Player> ();
	}

	void OnTriggerEnter2D (Collider2D entered)
	{
		player.AddApples (1 * (gold ? 5 : 1));

		Destroy (gameObject);
	}

    
	// ========================================================================================\\
}
