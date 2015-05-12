using UnityEngine;

public static class MaterialUtility
{

	public static void SetAlpha (this Material material, float value)
	{
		Color color = material.color;
		color.a = value;
		material.color = color;
	}

}
