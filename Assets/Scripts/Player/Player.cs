using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	// ========================================================================================\\

	public Sprite deathSprite;
	//
	private bool alive = true;
	private int score = 0;
	private int apples = 0;
	private int lives = 1;
	private bool poisoned;

	// ========================================================================================\\

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	// ========================================================================================\\

	public void Kill (PlayerDeathCause cause)
	{
		// set player to dead
		alive = false;

		GetComponentInChildren<SpriteRenderer>().sprite = deathSprite;



		GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300.0f);
	}

	public void Revive ()
	{
	}

	public void Poison()
	{
	}

	public void Crash()
	{
	}
	
	// ========================================================================================\\

	public void AddLives(int amt)
	{
		this.lives += amt;
	}

	public void RemoveLives(int amt)
	{
		this.lives -= amt;
	}

	public void AddApples(int amt)
	{
		this.apples += amt;
	}

	public void RemoveApples(int amt)
	{
		this.apples -= amt;
	}
	
	// ========================================================================================\\

	public void SetAlive (bool b)
	{
		this.alive = b;
	}

	public void SetScore (int n)
	{
		this.score = n;
	}

	public void SetApples (int n)
	{
		this.apples = n;
	}

	public void SetLives (int n)
	{
		this.lives = n;
	}

	public void SetPoisoned (bool b)
	{
		this.poisoned = b;
	} 


	// ========================================================================================\\


	public bool IsAlive ()
	{
		return alive;
	}

	public int GetScore ()
	{
		return score;
	}

	public int GetApples ()
	{
		return apples;
	}

	public int GetLives ()
	{
		return lives;
	}

	public bool IsPoisoned ()
	{
		return poisoned;
	}

	// ========================================================================================\\
}
