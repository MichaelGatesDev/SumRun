using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
	// ========================================================================================\\

	public GameObject mainGroup;
	public GameObject optionsGroup;

	// ========================================================================================\\


	// Use this for initialization
	void Start ()
	{
		mainGroup = GameObject.Find("MainGroup");
		optionsGroup = GameObject.Find ("OptionsGroup");
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	// ========================================================================================\\


	public void PressPlay ()
	{
		mainGroup.GetComponent<Animator>().SetBool("playing", true);
		StartCoroutine("StartGame", 0.4f);
	}

	public void PressOptions ()
	{
		optionsGroup.GetComponent<Animator>().SetBool("optioning", true);

	}

	public void PressBackFromOptions()
	{
		optionsGroup.GetComponent<Animator>().SetBool("optioning", false);
	}

	public void PressQuit ()
	{
		mainGroup.GetComponent<Animator>().SetBool("quitting", true);
		StartCoroutine("QuitGame", 0.4f);
	}

	public void PressDebug ()
	{
	}

	// ========================================================================================\\
	
	private void StartGame()
	{
		Application.LoadLevel("Level");
	}
	
	private void QuitGame()
	{
		Application.Quit ();
	}

	
	// ========================================================================================\\
}
