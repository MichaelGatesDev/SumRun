using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
	public string sceneName;

	void Start()
	{
		GetComponent<Button>().onClick.AddListener(() => LoadScene());
	}


	private void LoadScene ()
	{
		Application.LoadLevel (sceneName);
	}

}
