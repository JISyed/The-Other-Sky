// From Fortress Fiasco

using UnityEngine;
using System.Collections;

public class FadeOutScene : MonoBehaviour 
{
	public Texture2D screenImage = null;
	[SerializeField] private GameObject screenPrefab;
	public float fadeOutSpeedMultiplier = 1.0f;
	private float fade = 0.0f;

	private Color fadeColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
	private Rect screenRegion = new Rect();
	
	void Update()
	{
		if ( fade > 1.3f )
		{
			if(LevelController.EndSignal == LevelEndSignal.Died)
			{
				LevelController.SpawnPlayerAtLastSpawnPoint();	// Respawn player
			}
			else if(LevelController.EndSignal == LevelEndSignal.BeatLevel)
			{
				Instantiate(screenPrefab);
				Application.LoadLevel(Application.loadedLevel + 1);	// Load the next level
			}
			else if(LevelController.EndSignal == LevelEndSignal.BeatGame)
			{
				Instantiate(screenPrefab);
				Application.LoadLevel("MadeByScreen");	// Specifically load "MadeByScreen.unity"
			}
			else if(LevelController.EndSignal == LevelEndSignal.RestartGame)
			{
				Instantiate(screenPrefab);
				Application.LoadLevel(0);	// Load whatever the first level is
			}

			Destroy( gameObject );
		}
		else
		{
			fade += Time.deltaTime * fadeOutSpeedMultiplier;
		}
	}
	
	void OnGUI()
	{	
		GUI.depth = -1;

		fadeColor.a = fade;
		GUI.color = fadeColor;
		
		if ( screenImage != null )
		{
			screenRegion.Set(0, 0, Screen.width, Screen.height);
			GUI.DrawTexture(screenRegion, screenImage );
		}
	}

}
