using UnityEngine;
using System.Collections;

public class ButtonOptions : MonoBehaviour
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
		GameObject.Find ("MainCanvas").GetComponent<CanvasGroup>().alpha = 0;
		GameObject.Find ("OptionsCanvas").GetComponent<CanvasGroup>().alpha = 1;
	}
}
