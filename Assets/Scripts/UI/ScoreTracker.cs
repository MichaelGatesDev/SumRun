using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
	// ========================================================================================\\

	private Player player;
	public GameObject textObj;

	// ========================================================================================\\

	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find ("Player").GetComponent<Player> ();
	}
    
	// Update is called once per frame
	void Update ()
	{
		textObj.GetComponent<Text> ().text = player.GetApples() + "";
	}
    
	// ========================================================================================\\
}
