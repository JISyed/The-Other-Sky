using UnityEngine;
using System.Collections;

public class LockTrigger : MonoBehaviour 
{
	[SerializeField] private GameObject ballHologramPrefab;
	[SerializeField] private GameObject gateLockObject;			// Cannot be a prefab!

	// Use this for initialization
	void Start () 
	{
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Equals("GravityBall"))
		{
			Destroy(ballHologramPrefab);
			Destroy(gateLockObject);
			audio.Play();
			Destroy(gameObject, audio.clip.length);

		}
	}
}
