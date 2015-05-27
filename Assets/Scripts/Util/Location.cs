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
