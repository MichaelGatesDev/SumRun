using UnityEngine;
using System.Collections;

/* Used to follow deer on main menu */
public class SpotlightTracker : MonoBehaviour
{
	// ========================================================================================\\

	public float[] yWaypoints;
	public float startX;
	
	// ========================================================================================\\

	// move spotlight around deer npc
	public void Move (int waypoint)
	{
		float x = gameObject.transform.position.x;
		float z = gameObject.transform.position.z;
		Vector3 newPos = new Vector3 (x, yWaypoints [waypoint], z);
		gameObject.transform.position = newPos;
		GameObject.Find ("Deer NPC").transform.position = newPos;
	}
	
	// ========================================================================================\\
}
