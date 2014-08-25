using UnityEngine;
using System.Collections;

public class RestartWholeGame : MonoBehaviour 
{
	[SerializeField] private GameObject fadeOutPrefab;
	[SerializeField] private GameObject screenBoardObject;	// Cannot be a prefab!
	private bool disable = false;

	// Use this for initialization
	void Start () 
	{
		Screen.lockCursor = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!disable && Input.GetKeyDown(KeyCode.Return))
		{
			disable = true;
			LevelController.SetEndSignal(LevelEndSignal.RestartGame);
			Instantiate(fadeOutPrefab);
			Destroy(screenBoardObject, 1.11f);
		}
	}
}
