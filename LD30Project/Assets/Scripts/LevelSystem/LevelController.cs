using UnityEngine;
using System.Collections;

public enum LevelEndSignal
{
	Died,
	BeatLevel,
	BeatGame
}

public class LevelController : MonoBehaviour 
{
	private static LevelController instance;	// Private singleton
	private static LevelEndSignal endSignal = LevelEndSignal.Died;

	[SerializeField] private GameObject playerObject;		// Scene object
	[SerializeField] private GameObject fadeOutPrefab;		// Must be prefab

	private Vector3 spawnPosition;
	//private Quaternion spawnOrientation;

	// Use this for initialization
	void Start () 
	{
		LevelController.instance = this;
		spawnPosition = playerObject.transform.position;
		//spawnOrientation = playerObject.transform.rotation;
	}

	// Destructor
	void OnDestroy()
	{
		LevelController.instance = null;
	}

	// Does this singleton exist?
	private static bool DoesExist()
	{
		if(LevelController.instance == null)
		{
			Debug.LogError("LevelController does not exist!");
			return false;
		}

		return true;
	}

	public static LevelEndSignal EndSignal
	{
		get
		{
			return LevelController.endSignal;
		}
	}

	public static void SetEndSignal(LevelEndSignal newSignal)
	{
		LevelController.endSignal = newSignal;
	}

	public static void SetNewSpawnPoint(Transform newSpawnPoint)
	{
		if(LevelController.DoesExist() == false) return;

		LevelController.instance.spawnPosition = newSpawnPoint.position;
		//LevelController.instance.spawnOrientation = newSpawnPoint.rotation;
	}

	public static void SpawnPlayerAtLastSpawnPoint()
	{
		if(LevelController.DoesExist() == false) return;

		var player = LevelController.instance.playerObject.GetComponent<RigidbodyFPSController>();
		player.RemoveAllForces();

		LevelController.instance.playerObject.transform.position 
			= LevelController.instance.spawnPosition;

		//LevelController.instance.playerObject.transform.rotation 
		//	= LevelController.instance.spawnOrientation;
	}
}
