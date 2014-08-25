using UnityEngine;
using System.Collections;

public class EscapeGame : MonoBehaviour 
{
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();

			if(Application.platform == RuntimePlatform.OSXEditor 
			   || Application.platform == RuntimePlatform.WindowsEditor)
			{
				Debug.LogWarning("Application.Quit() doesn't work from the editor.");
			}
		}
	}
}
