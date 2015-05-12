using UnityEngine;
using System.Collections;

public class Metadata : MonoBehaviour
{

	public string metadata;
	
	public string GetMetadata ()
	{
		return metadata;
	}

	public void SetMetadata (string metadata)
	{
		this.metadata = metadata;
	}

}
