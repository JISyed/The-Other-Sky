using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour 
{
	public Color zoneColor; 
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			Debug.Log("Bleh!");
		}
	}
	
	void OnDrawGizmos()
	{
		// Draw transparent zone
		zoneColor.a = 0.5f;
		Gizmos.color = zoneColor;
		Gizmos.DrawCube(transform.position, transform.localScale);
		
		// Draw outline
		zoneColor.a = 1.0f;
		Gizmos.color = zoneColor;
		Gizmos.DrawWireCube(transform.position, transform.localScale);
	}
}
