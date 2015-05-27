using UnityEngine;
using System.Collections;

public class ButtonExit : MonoBehaviour {

	
	public void OnPress()
	{
		Application.LoadLevel("MainMenu");
	}

}
