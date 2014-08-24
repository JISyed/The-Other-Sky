// Credit: http://wiki.unity3d.com/index.php?title=RigidbodyFPSWalker
// And to quill18
// Modified to accommodate gravity flipping

using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class RigidbodyFPSController : MonoBehaviour 
{
	private Color gizmoColor = new Color(0.0f, 0.2f, 0.9f, 0.5f);
	private bool grounded = false;					// Is player touching the ground?
	private float pitchRotation = 0;				// Current camera rotation
	private float pitchRangeInDegrees = 60;			// Limits the camera rotation so you can't look back upside down
	private Vector3 targetVelocity = new Vector3();	// How fast we should be moving?
	private Vector3 jumpVector = new Vector3();		// Indicates jump magnitude and direction
	private Vector3 gravityVector = new Vector3();	// Indicates gravity magnitude and direction
	private bool isFlipping = false;				// Is the player in the process of flipping?
	private float flipTime = 0.75f;					// In seconds

	public float speed = 4.5f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 4.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	public float mouseSensitivity = 2;


	void Awake () 
	{
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
	}

	void Start ()
	{
		Screen.lockCursor = true;
	}

	// Draw gizmos
	void OnDrawGizmos()
	{
		Gizmos.color = gizmoColor;
		Gizmos.DrawSphere(transform.position, 0.5f);
		Gizmos.DrawSphere(transform.position + Vector3.up, 0.5f);
	}

	void Update()
	{
		if(!isFlipping)
		{
			this.RotatePlayer();
		}

		this.RotateCamera();

		if(Input.GetKeyDown(KeyCode.F) && !isFlipping)
		{
			GravityController.FlipGravity();

			StartCoroutine(FlipPlayer(flipTime));
		}
	}

	void FixedUpdate () 
	{
		// Calculate how fast we should be moving
		targetVelocity.Set(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		targetVelocity = transform.TransformDirection(targetVelocity);
		targetVelocity *= speed;
		
		// Apply a force that attempts to reach our target velocity
		Vector3 velocity = rigidbody.velocity;
		Vector3 velocityChange = (targetVelocity - velocity);
		velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
		velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
		velocityChange.y = 0;
		rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
		
		// Jump
		if (grounded && canJump && Input.GetKey(KeyCode.Space)) 
		{
			if(audio.isPlaying == false)
			{
				audio.Play();	// Assumes AudioSource is holding a jump sound
			}

			jumpVector.Set(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			rigidbody.velocity = jumpVector;
			//grounded = false;
		}
		
		// We apply gravity manually for more tuning control
		gravityVector.Set(0, -gravity * rigidbody.mass * GravityController.GravityPolarity, 0);
		rigidbody.AddForce(gravityVector);

		// Prevents double jumping
		grounded = false;
	}
	
	void OnCollisionStay (Collision other) 
	{
		// Check if the slope of the contact is too steep
		float slopeFactor = Mathf.Abs(Vector3.Dot(other.contacts[0].normal, transform.up));
		if(slopeFactor < 0.2f)
		{
			// Too steep, skip
			return;
		}

		grounded = true;    
	}
	
	private float CalculateJumpVerticalSpeed () 
	{
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity) * GravityController.GravityPolarity;
	}

	private void RotatePlayer()
	{
		float rotYaw = Input.GetAxis("Mouse X") * mouseSensitivity;
		transform.Rotate(0, rotYaw, 0);
	}

	private void RotateCamera()
	{
		pitchRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		pitchRotation = Mathf.Clamp(pitchRotation, -pitchRangeInDegrees, pitchRangeInDegrees);
		Camera.main.transform.localRotation = Quaternion.Euler(pitchRotation, 0 ,0);
	}

	// Thanks to "Mike 3"
	private IEnumerator FlipPlayer(float time)
	{
		float start = 0;
		float end = 180;
		float t = 0;
		float currAngle = start;
		float prevAngle = currAngle;

		// Flipping started
		isFlipping = true;

		while(t < 1)
		{
			yield return null;

			t += Time.deltaTime / time;

			// Budge the angle a little bit
			currAngle = Mathf.Lerp(start, end, t);
			transform.Rotate(transform.forward, currAngle - prevAngle, Space.World);
			prevAngle = currAngle;
		}

		// One final angle budge, just in case currAngle isn't 180
		currAngle = end;
		transform.Rotate(transform.forward, currAngle - prevAngle, Space.World);

		// Flipping officially ended
		isFlipping = false;

	}

	public void RemoveAllForces()
	{
		rigidbody.AddForce( -rigidbody.velocity, ForceMode.VelocityChange );
	}

	public void FlipPlayerOrientation()
	{
		StartCoroutine(FlipPlayer(flipTime));
	}
}