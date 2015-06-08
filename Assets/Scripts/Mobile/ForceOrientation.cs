using UnityEngine;
using System.Collections;

public class ForceOrientation : MonoBehaviour
{
	// ========================================================================================\\

	public enum OrientationMode
	{
		LANDSCAPE,
		PORTRAIT,
	}
	
	// ========================================================================================\\

	public OrientationMode mode; // the orientation 
	
	// ========================================================================================\\

	// Use this for initialization
	void Start ()
	{
		// if landscape mode
		if (mode == OrientationMode.LANDSCAPE) {
			// set landscape mode
			Screen.orientation = ScreenOrientation.LandscapeLeft;
		} else {
			// set portrait mode
			Screen.orientation = ScreenOrientation.Portrait;
		}
	}
	
	// ========================================================================================\\
}
