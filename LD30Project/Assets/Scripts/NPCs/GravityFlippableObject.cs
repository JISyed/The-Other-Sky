// Apply this script to any object that can be affected by gravity flipping

using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class GravityFlippableObject : MonoBehaviour 
{
	private Vector3 gravityVector = new Vector3();	// Indicates gravity magnitude and direction
	private Vector3 initialPosition;

	public float gravity = 10.0f;
	public bool freezeRotation = false;
	
	void Awake () 
	{
		rigidbody.freezeRotation = this.freezeRotation;
		rigidbody.useGravity = false;
	}
	
	void Start ()
	{
		initialPosition = transform.position;
	}
	
	void FixedUpdate () 
	{
		// We apply gravity manually for more tuning control
		gravityVector.Set(0, -gravity * rigidbody.mass * GravityController.GravityPolarity, 0);
		rigidbody.AddForce(gravityVector);
	}

	public void Respawn()
	{
		transform.position = initialPosition;
	}
}
