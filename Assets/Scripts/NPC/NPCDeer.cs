using UnityEngine;
using System.Collections;

public class NPCDeer : MonoBehaviour
{
	public bool running;
	float runSpeed = 5.0f;


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(running)
		{
			transform.Translate(Vector2.right * Time.deltaTime * runSpeed);
		}
	}

}
