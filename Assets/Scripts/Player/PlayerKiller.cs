using UnityEngine;
using System.Collections;

public class PlayerKiller : MonoBehaviour
{
    // ========================================================================================\\

    private LevelManager lm;
    
    // ========================================================================================\\

    void OnTriggerEnter2D(Collider2D coll)
    {
		// if object that entered is not the player
        if (coll.gameObject.name != "Player")
            return;

		// kill the player
		coll.gameObject.GetComponent<Player>().Kill(PlayerDeathCause.FALL);
    }

    // ========================================================================================\\
}
