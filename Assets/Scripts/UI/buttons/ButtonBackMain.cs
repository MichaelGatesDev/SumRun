using UnityEngine;
using System.Collections;

public class ButtonBackMain : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	public void OnPress ()
	{
		GameObject.Find ("MainCanvas").GetComponent<CanvasGroup> ().alpha = 1;
		GameObject.Find ("OptionsCanvas").GetComponent<CanvasGroup> ().alpha = 0;
	}

}
