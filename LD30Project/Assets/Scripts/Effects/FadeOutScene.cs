// From Fortress Fiasco

using UnityEngine;
using System.Collections;

public class FadeOutScene : MonoBehaviour 
{
	public Texture2D screenImage = null;
	public float fadeOutSpeedMultiplier = 1.0f;
	private float fade = 0.0f;

	private Color fadeColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
	private Rect screenRegion = new Rect();
	
	void Update()
	{
		if ( fade > 1.0f )
		{
			if(LevelController.EndSignal == LevelEndSignal.Died)
			{
				LevelController.SpawnPlayerAtLastSpawnPoint();
			}
			else if(LevelController.EndSignal == LevelEndSignal.BeatLevel)
			{
				// TODO: IMPLEMENT
			}
			else if(LevelController.EndSignal == LevelEndSignal.BeatGame)
			{
				// TODO: IMPLEMENT
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
