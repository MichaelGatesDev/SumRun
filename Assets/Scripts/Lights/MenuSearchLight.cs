using UnityEngine;
using System.Collections;

public class MenuSearchLight : MonoBehaviour
{
	// ========================================================================================\\

	public Transform startPos;
	public Transform endPos;
	public float speed = 1.0f;
	public float behindPadding = 10.0f;
	
	// ========================================================================================\\
	
	// Update is called once per frame
	void Update ()
	{
		// if positions are null, ignore
		if (startPos == null || endPos == null)
			return;

		// as long as the light hasn't reached the end
		if (transform.position.x < endPos.position.x) {
			// move it to the right
			transform.Translate (Vector3.right * Time.deltaTime * speed);
			// reached the end of the path
		} else {
			// reset position to beginning
			transform.position = new Vector3 (startPos.position.x - behindPadding, startPos.position.y, startPos.position.z);
		}
		
	}
	
	// ========================================================================================\\
}