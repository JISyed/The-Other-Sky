using UnityEngine;
using System.Collections;

public enum NextLevelSignal
{
	NextLevel,
	EndGame
}

public class NextLevelTrigger : MonoBehaviour 
{
	[SerializeField] private Color zoneColor;
	[SerializeField] private GameObject fadeOutQuickPrefab;
	[SerializeField] private GameObject fadeOutSlowPrefab;
	[SerializeField] private AudioClip levelEndSound;
	[SerializeField] private AudioClip gameEndSound;
	[SerializeField] private NextLevelSignal signal;

	private bool alreadyTriggered = false;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag.Equals("Player") && !alreadyTriggered)
		{
			alreadyTriggered = true;

			if(signal == NextLevelSignal.NextLevel)
			{
				LevelController.SetEndSignal(LevelEndSignal.BeatLevel);
				audio.clip = levelEndSound;
				audio.Play();
				Instantiate(fadeOutQuickPrefab);
			}
			else if(signal == NextLevelSignal.EndGame)
			{
				LevelController.SetEndSignal(LevelEndSignal.BeatGame);
				audio.clip = gameEndSound;
				audio.Play();
				Instantiate(fadeOutSlowPrefab);
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
