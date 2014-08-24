using UnityEngine;
using System.Collections;

public class FlippingDeviceTrigger : MonoBehaviour 
{
	[SerializeField] private GameObject lightRodPrefab;
	[SerializeField] private GameObject lightSourcePrefab;
	[SerializeField] private GameObject outerRingPrefab;
	[SerializeField] private GameObject innerRingPrefab;
	[SerializeField] private Material unlitRodMaterial;
	[SerializeField] private bool alwaysActivated;		// If true, this can be used infinitely many times
	private bool usedOnce = false;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	void Update()
	{
		if(usedOnce)
		{
			usedOnce = false;
			lightRodPrefab.renderer.material = unlitRodMaterial;
			lightSourcePrefab.light.enabled = false;
			Destroy(outerRingPrefab);
			Destroy(innerRingPrefab);
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag.Equals("Player"))
		{
			GravityController.FlipGravity();

			var player = other.gameObject.GetComponent<RigidbodyFPSController>();
			player.FlipPlayerOrientation();

			if(!alwaysActivated)
			{
				usedOnce = true;
			}
		}
	}
}
