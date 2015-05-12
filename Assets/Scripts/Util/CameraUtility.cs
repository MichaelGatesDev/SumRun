using UnityEngine;
using System.Collections;

public class CameraUtility : MonoBehaviour
{
	// ========================================================================================\\

	public static void MoveMain (Vector3 pos)
	{
		Camera.main.transform.position = pos;
	}

	
	// ========================================================================================\\
}
