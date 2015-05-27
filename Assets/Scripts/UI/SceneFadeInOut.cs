using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFadeInOut : MonoBehaviour
{
	public Color startColor;
	public Color endColor;
	public float duration;
	private RawImage ren;
	private bool entering;
	private float t = 0;
	
	// Use this for initialization
	void Start ()
	{
		ren = GetComponent<RawImage> ();
		entering = true;
	}

	void OnSceneLoad ()
	{
		entering = true;
	}
	
	void Update ()
	{
		if (entering) {
			ren.material.color = Color.Lerp (startColor, endColor, t);
			
			if (t < 1) {
				t += Time.deltaTime / duration;
			}
			
			if (ren.material.color == endColor) {
				entering = false;
				t = 0;
			}
		}
	}
}