using UnityEngine;
using System.Collections;

public class Indestructable : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		// makes object persistent
		DontDestroyOnLoad(gameObject);
	}

}
