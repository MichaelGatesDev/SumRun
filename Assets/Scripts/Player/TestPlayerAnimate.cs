using UnityEngine;
using System.Collections;

public class TestPlayerAnimate : MonoBehaviour
{
	private Animator anim;
	private bool jumping;
	private bool sliding;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
	}
    
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyUp (KeyCode.Space)) {
			Jump ();
		} else if (Input.GetKeyUp (KeyCode.LeftShift)) {
			Slide ();
		} else {
			//Run();
		}
	}

	private void Jump ()
	{
		if (jumping)
			return;

		jumping = true;

		anim.SetBool ("jumping", true);

		Debug.Log ("Jump");
	}
    
	private void Slide ()
	{
		if (sliding)
			return;

		sliding = true;

		anim.SetBool ("sliding", true);
       
		Debug.Log ("Slide");
	}
    
	private void Run ()
	{
		if (anim.GetInteger ("state") == 0)
			return;

		anim.SetInteger ("state", 0);
        
		Debug.Log ("Run");
	}

}

