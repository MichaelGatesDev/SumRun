using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
	// ========================================================================================\\

	Animator anim;
	Rigidbody2D rb;
	bool grounded;
	bool justJumped;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;
	bool sliding;

	float runSpeed = 5.0f;

	public GameObject groundEffect;
	
	// ========================================================================================\\

	// Use this for initialization
	void Start ()
	{
		anim = GetComponentInChildren<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//TODO: temporary controls for debug only, not mobile

		if (Input.GetKeyUp (KeyCode.Space)) {
			Jump ();
		} else if (Input.GetKeyUp (KeyCode.LeftShift)) {
			Slide ();
		}

		Run();
	}

	void FixedUpdate ()
	{
		float vSpeed = rb.velocity.y;
		anim.SetFloat ("vSpeed", vSpeed);

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("grounded", grounded);

		anim.SetBool ("sliding", sliding);
	}

	
	// ========================================================================================\\

	private void Jump ()
	{
		// can only jump if on the ground
		if (grounded) {
			// can not jump if sliding
			if (!sliding) {
				rb.AddForce (Vector2.up * 300.0f);
				justJumped = true;
				InvokeRepeating("GroundTouch", 0.1f, 0.1f);
			}
		}
	}

	private void Slide ()
	{
		// can only slide if grounded
		if (grounded) {
			// if not already sliding
			if (!sliding) {
				sliding = true;
				Invoke ("EndSlide", 1.5f);
			}
		}
	}

	private void Run()
	{
		transform.Translate(Vector2.right * Time.deltaTime * runSpeed);
	}

	private void GroundTouch()
	{
		if(grounded)
		{
			justJumped = false;
			CancelInvoke("GroundTouch");
			PlayGroundEffect();
		}
	}

	private void EndSlide ()
	{
		sliding = false;
	}


	private void PlayGroundEffect()
	{
		GameObject go = (GameObject) Instantiate(groundEffect, transform.position, Quaternion.identity);
	}
	
	// ========================================================================================\\
}
