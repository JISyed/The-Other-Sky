using UnityEngine;
using System.Collections;

public static class GravityController
{
	private static int gravityPolarity = 1;

	public static void FlipGravity()
	{
		gravityPolarity *= -1;
	}

	public static int GravityPolarity
	{
		get
		{
			return GravityController.gravityPolarity;
		}
	}
}
