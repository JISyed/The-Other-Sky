using UnityEngine;
using System.Collections;

public static class GravityController
{
	// If positive, gravity is pointing downward
	// If negative, gravity is pointing upward
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
