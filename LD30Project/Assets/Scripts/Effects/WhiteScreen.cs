using UnityEngine;
using System.Collections;

public class WhiteScreen : MonoBehaviour 
{
	private Vector3 initialPosition = new Vector3();

	// Use this for initialization
	void Start () 
	{
		initialPosition.Set(0.48f, 0.61f, 0.0f);
		transform.position = initialPosition;
		DontDestroyOnLoad(gameObject);
		Destroy(gameObject, 0.01f);
	}
}
