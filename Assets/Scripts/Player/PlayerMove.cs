using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
	Animator anim;
	Rigidbody2D rb;
	bool grounded;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;
	bool sliding;

	float runSpeed = 5.0f;


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

	private void Jump ()
	{
		// can only jump if on the ground
		if (grounded) {
			// can not jump if sliding
			if (!sliding) {
				rb.AddForce (Vector2.up * 300.0f);
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
				StartCoroutine ("EndSlide");
			}
		}
	}

	private void Run()
	{
		transform.Translate(Vector2.right * Time.deltaTime * runSpeed);
	}

	private IEnumerator EndSlide ()
	{
		while (true) {
			yield return new WaitForSeconds (1.5f);
			sliding = false;
			break;
		}
		yield break;
	}

}
