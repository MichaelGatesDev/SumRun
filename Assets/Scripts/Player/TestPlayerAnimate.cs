using UnityEngine;
using System.Collections;

public class TestPlayerAnimate : MonoBehaviour
{
	// =========================================================== \\

	private Animator anim;
	private bool jumping;
	private bool sliding;
	public float runSpeed = 5.0f;
	
	// =========================================================== \\

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
			Run ();
		}
	}

	// =========================================================== \\


	private void Jump ()
	{
		if (jumping)
			return;

		jumping = true;

		anim.SetBool ("jumping", true);

		GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 300.0f);

		StartCoroutine ("CheckFinishJump");
	}
    
	private void Slide ()
	{
		if (sliding)
			return;

		sliding = true;

		anim.SetBool ("sliding", true);
       
		StartCoroutine ("CheckFinishSlide");

		StartCoroutine ("SlideColliders");

	}
    
	private void Run ()
	{
		gameObject.transform.Translate (Vector2.right * Time.deltaTime * runSpeed); // move the player

		if (anim.GetBool ("running"))
			return;

		anim.SetBool ("running", true);
	}
	
	private IEnumerator CheckFinishJump ()
	{
		while (true) {
			yield return new WaitForSeconds (0.1f);
			if (!jumping) {
				anim.SetBool ("jumping", false);
				break;
			}
		}
		yield break;
	}
	
	private IEnumerator CheckFinishSlide ()
	{
		while (true) {
			yield return new WaitForSeconds (1.0f);
			sliding = false;
			anim.SetBool ("sliding", false);
			break;
		}
		yield break;
	}

	private IEnumerator SlideColliders ()
	{
		while (true) {
			yield return new WaitForSeconds (0.4f);
			
			transform.FindChild ("BigCollider").GetComponent<BoxCollider2D> ().enabled = false;
			transform.FindChild ("SmallCollider").GetComponent<BoxCollider2D> ().enabled = true;

			yield return new WaitForSeconds (1.4f);

			transform.FindChild ("BigCollider").GetComponent<BoxCollider2D> ().enabled = true;
			transform.FindChild ("SmallCollider").GetComponent<BoxCollider2D> ().enabled = false;

			break;
		}
		yield break;
	}
	
	// =========================================================== \\

	public void SetJumping (bool b)
	{
		this.jumping = b;
	}

	// =========================================================== \\
}


