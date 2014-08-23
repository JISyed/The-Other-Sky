// From Fortress Fiasco

using UnityEngine;
using System.Collections;

public class FadeInScene : MonoBehaviour 
{
	public Texture2D screenImage = null;
	//public GameObject fadeIn;
	
	private float fade = 1.0f;
	public float fadeOutSpeedMultiplier = 4.0f;

	private Color fadeColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	private Rect screenRegion = new Rect();

	/*
	void Awake()
	{
		DontDestroyOnLoad( this );
	}
	*/
	
	void Update()
	{
		if (fade <= 0)
		{
			Destroy(gameObject);
		}
		else
		{
			fade -= Time.deltaTime * fadeOutSpeedMultiplier;
		}
	}
	
	void OnGUI()
	{	
		GUI.depth = -1;

		fadeColor.a = fade;
		GUI.color = fadeColor;
		
		if (screenImage != null)
		{
			screenRegion.Set(0, 0, Screen.width, Screen.height);
			GUI.DrawTexture(screenRegion, screenImage);
		}
	}

}
