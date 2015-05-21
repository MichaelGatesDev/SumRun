using UnityEngine;
using System.Collections;

public class Location
{
	
	// ========================================================================================\\

	private float x;
	private float y;
	private float z;
	private Biome biome;
	
	// ========================================================================================\\

	public Location ()
	{
	}

	public Location (float x, float y, float z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public Location (float x, float y, float z, Biome b)
	{
		this.x = x;
		this.y = y;
		this.z = z;
		this.biome = b;
	}
	
	public Location (Transform transform)
	{
		this.x = transform.position.x;
		this.y = transform.position.y;
		this.z = transform.position.z;
	}
	
	public Location (Transform transform, Biome b)
	{
		this.x = transform.position.x;
		this.y = transform.position.y;
		this.z = transform.position.z;
		this.biome = b;
	}

	// ========================================================================================\\

	
	public void SetX (float f)
	{
		this.x = f;
	}

	public void SetY (float f)
	{
		this.y = f;
	}

	public void SetZ (float f)
	{
		this.z = f;
	}

	public void SetBiome (Biome biome)
	{
		this.biome = biome;
	}

	
	// ========================================================================================\\


	public float GetX ()
	{
		return x;
	}

	public float GetY ()
	{
		return y;
	}

	public float GetZ ()
	{
		return z;
	}

	public Biome GetBiome ()
	{
		return biome;
	}


	// ========================================================================================\\
}
