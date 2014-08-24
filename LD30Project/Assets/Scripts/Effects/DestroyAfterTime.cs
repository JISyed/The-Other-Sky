using UnityEngine;

public class DestroyAfterTime : MonoBehaviour 
{
	public float timeInSeconds;

	// Use this for initialization
	void Start () 
	{
		Destroy(gameObject, timeInSeconds);
	}

}
