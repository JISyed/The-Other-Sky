// Credit to quill18

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour 
{
	private CharacterController cc;
	private Vector3 velocity;
	public float moveSpeed = 5;
	public float mouseSensitivity = 5;

	private float pitchRotation = 0;
	private float pitchRangeInDegrees = 60;

	private float verticalVelocity = 0;
	public float jumpStrength = 10;

	private Color gizmoColor = new Color(0.0f, 0.2f, 0.9f, 0.5f);

	// Use this for initialization
	void Start () 
	{
		Screen.lockCursor = true;
		this.velocity = new Vector3();
		this.cc = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Rotation
		this.RotatePlayer();
		this.RotateCamera();

		// Movement
		this.MovePlayer();

		if(Input.GetKeyDown(KeyCode.G))
		{
			GravityController.FlipGravity();
		}
	}

	// Draw gizmos
	void OnDrawGizmos()
	{
		Gizmos.color = gizmoColor;
		Gizmos.DrawSphere(transform.position, 0.5f);
		Gizmos.DrawSphere(transform.position + Vector3.up, 0.5f);
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

	private void MovePlayer()
	{
		float forwardSpeed = Input.GetAxis("Vertical") * moveSpeed;
		float sideSpeed = Input.GetAxis("Horizontal") * moveSpeed;
		
		verticalVelocity += Physics.gravity.y * Time.deltaTime * GravityController.GravityPolarity;
		
		if(Input.GetButtonDown("Jump") && cc.isGrounded)
		{
			verticalVelocity = jumpStrength * GravityController.GravityPolarity;
		}
		
		this.velocity.Set(sideSpeed, verticalVelocity, forwardSpeed);
		
		// Turn the velocity vector
		velocity = transform.rotation * velocity;
		
		this.cc.Move(this.velocity * Time.deltaTime);

		if (cc.isGrounded)
			print("We are grounded");
	}
}
