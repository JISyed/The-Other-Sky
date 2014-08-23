// From Fortress Fiasco

using UnityEngine;
using System.Collections;

public class FadeOutScene : MonoBehaviour 
{
	public Texture2D screenImage = null;
	//public GameObject fadeIn;
	
	//[HideInInspector] public string levelToGoTo = "";
	
	public float fadeOutSpeedMultiplier = 4.0f;
	private float fade = 0.0f;

	private Color fadeColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
	private Rect screenRegion = new Rect();
	
	void Update()
	{
		if ( fade > 1.0f )
		{
			//GameObject temp;
			
			//Application.LoadLevel( levelToGoTo );
			
			//temp = (GameObject)Instantiate( fadeIn );
			
			//Destroy( gameObject );
		}
		else
			fade += Time.deltaTime * fadeOutSpeedMultiplier;
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
