using UnityEngine;
using System.Collections;

public class SpinningLogo : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
	{
		transform.Rotate(Vector3.up, Time.deltaTime * 80.0f);
	}
}
