//------------------------------------------------------------------------------
//  Author:
//       Michael Gates <michaelgatesdev@gmail.com>
//
//  Copyright (c) 2015 Michael Gates 2015
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------
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
	private GameObject walkCollider;
	private GameObject slideCollider;
	private GameObject jumpCollider;
	private GameObject fallCollider;
	
	// ========================================================================================\\

	// Use this for initialization
	void Start ()
	{
		anim = GetComponentInChildren<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		player = GameObject.Find ("Player").GetComponent<Player> ();
		
		walkCollider = GameObject.Find ("WalkCollider");
		jumpCollider = GameObject.Find ("JumpCollider");
		fallCollider = GameObject.Find ("FallCollider");
		slideCollider = GameObject.Find ("SlideCollider");
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


		// not on ground
		if (!grounded) {
			// running
			if (rb.velocity.y > 0) {
				DisableAllWalkColliders ();
				EnableAllJumpColliders ();
				DisableAllFallColliders ();
			}
			// falling
			else if (rb.velocity.y < 0) {
				DisableAllWalkColliders ();
				EnableAllFallColliders ();
				DisableAllJumpColliders ();
			}
		}
		// grounded
		else {
			EnableAllWalkColliders ();
			DisableAllFallColliders ();
			DisableAllJumpColliders ();
		}

	}

	void FixedUpdate ()
	{
		if (player == null)
			return;

		anim.SetBool ("dead", !player.IsAlive ());

		if (!player.IsAlive ())
			return;

		float vSpeed = rb.velocity.y;
		anim.SetFloat ("vSpeed", vSpeed);

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("grounded", grounded);

		anim.SetBool ("sliding", sliding);
	}

	
	// ========================================================================================\\

	private void Jump ()
	{
		if (player == null || !player.IsAlive ())
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
		if (player == null || !player.IsAlive ())
			return;

		// can only slide if grounded
		if (grounded) {
			// if not already sliding
			if (!sliding) {
				sliding = true;
				PlaySlideEffect ();
				Invoke ("EndSlide", 1.5f);
			}
		}
	}

	private void Run ()
	{
		if (player == null || !player.IsAlive ())
			return;

		transform.Translate (Vector2.right * Time.deltaTime * runSpeed);
	}

	private void GroundTouch ()
	{
		if (player == null || !player.IsAlive ())
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
		
		GameObject go = (GameObject)Instantiate (slideEffect, transform.position + new Vector3 (1.0f, 0, 0), Quaternion.identity);
		Destroy (go, 1.0f);
	}

	private void PlayCrashEffect ()
	{
		if (!player.IsAlive ())
			return;
		
		GameObject go = (GameObject)Instantiate (groundEffect, transform.position, Quaternion.identity);
		Destroy (go, 1.0f);
	}
	
	private void EnableAllJumpColliders ()
	{
		foreach (Collider2D co in jumpCollider.GetComponentsInChildren<BoxCollider2D>()) {
			co.enabled = true;
		}
		foreach (Collider2D co in jumpCollider.GetComponentsInChildren<CircleCollider2D>()) {
			co.enabled = true;
		}
		foreach (Collider2D co in jumpCollider.GetComponentsInChildren<EdgeCollider2D>()) {
			co.enabled = true;
		}
	}

	private void DisableAllJumpColliders ()
	{
		foreach (Collider2D co in jumpCollider.GetComponentsInChildren<BoxCollider2D>()) {
			co.enabled = false;
		}
		foreach (Collider2D co in jumpCollider.GetComponentsInChildren<CircleCollider2D>()) {
			co.enabled = false;
		}
		foreach (Collider2D co in jumpCollider.GetComponentsInChildren<EdgeCollider2D>()) {
			co.enabled = false;
		}
	}

	private void EnableAllFallColliders ()
	{
		foreach (Collider2D co in fallCollider.GetComponentsInChildren<BoxCollider2D>()) {
			co.enabled = true;
		}
		foreach (Collider2D co in fallCollider.GetComponentsInChildren<CircleCollider2D>()) {
			co.enabled = true;
		}
		foreach (Collider2D co in fallCollider.GetComponentsInChildren<EdgeCollider2D>()) {
			co.enabled = true;
		}
	}

	private void DisableAllFallColliders ()
	{
		foreach (Collider2D co in fallCollider.GetComponentsInChildren<BoxCollider2D>()) {
			co.enabled = false;
		}
		foreach (Collider2D co in fallCollider.GetComponentsInChildren<CircleCollider2D>()) {
			co.enabled = false;
		}
		foreach (Collider2D co in fallCollider.GetComponentsInChildren<EdgeCollider2D>()) {
			co.enabled = false;
		}
	}
	
	private void EnableAllWalkColliders ()
	{
		foreach (Collider2D co in walkCollider.GetComponentsInChildren<BoxCollider2D>()) {
			co.enabled = true;
		}
		foreach (Collider2D co in walkCollider.GetComponentsInChildren<CircleCollider2D>()) {
			co.enabled = true;
		}
	}
	
	private void DisableAllWalkColliders ()
	{
		foreach (Collider2D co in walkCollider.GetComponentsInChildren<BoxCollider2D>()) {
			co.enabled = false;
		}
		foreach (Collider2D co in walkCollider.GetComponentsInChildren<CircleCollider2D>()) {
			co.enabled = false;
		}
	}

	private void EnableAllSlideColliders ()
	{
		foreach (Collider2D co in slideCollider.GetComponentsInChildren<BoxCollider2D>()) {
			co.enabled = true;
		}
		foreach (Collider2D co in slideCollider.GetComponentsInChildren<CircleCollider2D>()) {
			co.enabled = true;
		}
	}
	
	private void DisableAllSlideColliders ()
	{
		foreach (Collider2D co in slideCollider.GetComponentsInChildren<BoxCollider2D>()) {
			co.enabled = false;
		}
		foreach (Collider2D co in slideCollider.GetComponentsInChildren<CircleCollider2D>()) {
			co.enabled = false;
		}
	}
	
	// ========================================================================================\\
}
