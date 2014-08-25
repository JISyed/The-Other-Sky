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
	private bool used = false;
	private int usedTimer = 0;
	private Material originalRodMaterial;

	// Use this for initialization
	void Start () 
	{
		originalRodMaterial = lightRodPrefab.renderer.material;
	}
	
	void Update()
	{
		if(usedOnce)
		{
			usedOnce = false;
			//lightRodPrefab.renderer.material = unlitRodMaterial;
			//lightSourcePrefab.light.enabled = false;
			Destroy(outerRingPrefab);
			Destroy(innerRingPrefab);
			Destroy(gameObject);
		}

		if(used && alwaysActivated)
		{
			usedTimer++;
			if(usedTimer > 60)
			{
				usedTimer = 0;
				used = false;
				lightRodPrefab.renderer.material = originalRodMaterial;
				lightSourcePrefab.light.enabled = true;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag.Equals("Player"))
		{
			used = true;
			lightRodPrefab.renderer.material = unlitRodMaterial;
			lightSourcePrefab.light.enabled = false;

			GravityController.FlipGravity();

			lightRodPrefab.audio.Play();
			PlayAmbientMusic.ReplayCorrectMusic();

			var player = other.gameObject.GetComponent<RigidbodyFPSController>();
			player.FlipPlayerOrientation();

			if(!alwaysActivated)
			{
				usedOnce = true;
			}
		}
	}
}
