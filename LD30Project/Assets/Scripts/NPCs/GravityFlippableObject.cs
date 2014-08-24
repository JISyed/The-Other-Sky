﻿using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class GravityFlippableObject : MonoBehaviour 
{
	private Vector3 gravityVector = new Vector3();	// Indicates gravity magnitude and direction

	public float gravity = 10.0f;
	
	void Awake () 
	{
		//rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
	}
	
	void Start ()
	{

	}
	
	void FixedUpdate () 
	{
		// We apply gravity manually for more tuning control
		gravityVector.Set(0, -gravity * rigidbody.mass * GravityController.GravityPolarity, 0);
		rigidbody.AddForce(gravityVector);
	}

}
