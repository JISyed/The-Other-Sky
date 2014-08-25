using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour 
{
	[SerializeField] private Color zoneColor;
	[SerializeField] private GameObject fadeOutPrefab;	// Must be a prefab
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag.Equals("Player"))
		{
			audio.Play();
			LevelController.SetEndSignal(LevelEndSignal.Died);
			Instantiate(fadeOutPrefab);
		}

		else if (other.gameObject.tag.Equals("GravityObject"))
		{
			var gravityObject = other.gameObject.GetComponent<GravityFlippableObject>();
			if(gravityObject != null)
			{
				gravityObject.Respawn();
			}
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
