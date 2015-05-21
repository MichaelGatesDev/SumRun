using UnityEngine;
using System.Collections;

public class CrashObstacle : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.gameObject.name == "Player")
		{
			Player player = GameObject.Find("Player").GetComponent<Player>();
			if(!player.IsAlive())
				return;

			player.Kill(PlayerDeathCause.CRASH);
		}
	}



}
