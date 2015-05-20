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
        if (coll.gameObject.name != "WalkCollider")
            return;

		// kill the player
		coll.transform.parent.GetComponent<Player>().Kill(PlayerDeathCause.FALL);
    }

    // ========================================================================================\\
}
