using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour 
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
			Debug.Log("Bam!");
		}
	}

	void OnDrawGizmos()
	{
		// Draw transparent zone
		float originalRed = zoneColor.r;
		float originalGreen = zoneColor.g;
		float originalBlue = zoneColor.b;
		zoneColor.r = originalRed;
		zoneColor.g = originalGreen;
		zoneColor.b = originalBlue;
		zoneColor.a = 0.5f;
		Gizmos.color = zoneColor;
		Gizmos.DrawCube(transform.position, transform.localScale);

		// Draw outline
		zoneColor.a = 1.0f;
		Gizmos.color = zoneColor;
		Gizmos.DrawWireCube(transform.position, transform.localScale);

		// Draw spawnpoint center
		zoneColor.r = 1.0f;
		zoneColor.g = 0.1f;
		zoneColor.b = 0.5f;
		Gizmos.color = zoneColor;
		Gizmos.DrawSphere(transform.position, 0.15f);
		zoneColor.r = originalRed;
		zoneColor.g = originalGreen;
		zoneColor.b = originalBlue;
	}
}
