// Credit: http://wiki.unity3d.com/index.php?title=RigidbodyFPSWalker
// And to quill18

using UnityEngine;

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
		this.RotatePlayer();
		this.RotateCamera();

		if(Input.GetKeyDown(KeyCode.G))
		{
			GravityController.FlipGravity();

			transform.Rotate(Vector3.right, 180, Space.World);

			if(GravityController.GravityPolarity > 0)
			{
				//Debug.Log("Rightside Up");
			}
			else if(GravityController.GravityPolarity < 0)
			{
				//Debug.Log("Upside Down");
			}
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
		if (grounded && canJump && Input.GetButtonDown("Jump")) 
		{
			jumpVector.Set(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			rigidbody.velocity = jumpVector;
		}
		
		// We apply gravity manually for more tuning control
		gravityVector.Set(0, -gravity * rigidbody.mass * GravityController.GravityPolarity, 0);
		rigidbody.AddForce(gravityVector);
		
		grounded = false;
	}
	
	void OnCollisionStay () 
	{
		grounded = true;    
	}
	
	float CalculateJumpVerticalSpeed () 
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
}