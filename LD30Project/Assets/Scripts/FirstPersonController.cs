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

	private float rotPitch = 0;
	public float pitchRangeDegrees = 60;

	float verticalVelocity = 0;
	public float jumpStrength = 10;

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
		float rotYaw = Input.GetAxis("Mouse X") * mouseSensitivity;
		transform.Rotate(0, rotYaw, 0);

		rotPitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		rotPitch = Mathf.Clamp(rotPitch, -pitchRangeDegrees, pitchRangeDegrees);
		Camera.main.transform.localRotation = Quaternion.Euler(rotPitch, 0 ,0);

		// Movement
		float forwardSpeed = Input.GetAxis("Vertical") * moveSpeed;
		float sideSpeed = Input.GetAxis("Horizontal") * moveSpeed;

		verticalVelocity += Physics.gravity.y * Time.deltaTime;

		if(Input.GetButtonDown("Jump") && cc.isGrounded)
		{
			verticalVelocity = jumpStrength;
		}

		this.velocity.Set(sideSpeed, verticalVelocity, forwardSpeed);

		// Turn the velocity vector
		velocity = transform.rotation * velocity;

		this.cc.Move(this.velocity * Time.deltaTime);
	}

	// Draw gizmos
	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		//Gizmos.color.a = 0.5f;
		Gizmos.DrawSphere(transform.position, 0.5f);
		Gizmos.DrawSphere(transform.position + Vector3.up, 0.5f);
	}
}
