using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
	// ========================================================================================\\

	public Transform groundCheck;
	public LayerMask whatIsGround;
	public float runSpeed = 5.0f;
	public GameObject groundEffect;
	public GameObject slideEffect;
	public GameObject crashEffect;
	//
	private Player player;
	private Animator anim;
	private Rigidbody2D rb;
	private bool grounded;
	private float groundRadius = 0.1f;
	private bool sliding;
	
	// ========================================================================================\\

	// Use this for initialization
	void Start ()
	{
		anim = GetComponentInChildren<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		player = GameObject.Find ("Player").GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//TODO: temporary controls for debug only, not mobile

		// if press space, jump
		if (Input.GetKeyUp (KeyCode.Space)) {
			Jump ();
		}
		// if press shift 
		else if (Input.GetKeyUp (KeyCode.LeftShift)) {
			Slide ();
		}

		Run ();
	}

	void FixedUpdate ()
	{
		float vSpeed = rb.velocity.y;
		anim.SetFloat ("vSpeed", vSpeed);

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("grounded", grounded);

		anim.SetBool ("sliding", sliding);

		anim.SetBool ("dead", !player.IsAlive ());
	}

	
	// ========================================================================================\\

	private void Jump ()
	{
		if (!player.IsAlive ())
			return;

		// can only jump if on the ground
		if (grounded) {
			// can not jump if sliding
			if (!sliding) {
				rb.AddForce (Vector2.up * 300.0f);
				InvokeRepeating ("GroundTouch", 0.1f, 0.1f);
			}
		}
	}

	private void Slide ()
	{
		if (!player.IsAlive ())
			return;

		// can only slide if grounded
		if (grounded) {
			// if not already sliding
			if (!sliding) {
				sliding = true;
				PlaySlideEffect();
				Invoke ("EndSlide", 1.5f);
			}
		}
	}

	private void Run ()
	{
		if (!player.IsAlive ())
			return;

		transform.Translate (Vector2.right * Time.deltaTime * runSpeed);
	}

	private void GroundTouch ()
	{
		if (!player.IsAlive ())
			return;

		if (grounded) {
			CancelInvoke ("GroundTouch");
			PlayGroundEffect ();
		}
	}

	private void EndSlide ()
	{
		if (!player.IsAlive ())
			return;

		sliding = false;
	}

	private void PlayGroundEffect ()
	{
		if (!player.IsAlive ())
			return;

		GameObject go = (GameObject)Instantiate (groundEffect, transform.position, Quaternion.identity);
		Destroy (go, 1.0f);
	}

	private void PlaySlideEffect ()
	{
		if (!player.IsAlive ())
			return;
		
		GameObject go = (GameObject)Instantiate (slideEffect, transform.position + new Vector3(1.0f,0,0), Quaternion.identity);
		Destroy (go, 1.0f);
	}

	private void PlayCrashEffect ()
	{
		if (!player.IsAlive ())
			return;
		
		GameObject go = (GameObject)Instantiate (groundEffect, transform.position, Quaternion.identity);
		Destroy (go, 1.0f);
	}
	
	// ========================================================================================\\
}
