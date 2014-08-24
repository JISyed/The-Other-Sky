using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayAmbientMusic : MonoBehaviour 
{
	private static PlayAmbientMusic instance;		// Private singleton

	[SerializeField] private AudioClip downWorldMusic;
	[SerializeField] private AudioClip upWorldMusic;


	// Use this for initialization
	void Start () 
	{
		PlayAmbientMusic.instance = this;
		StartPlayingMusic();
	}

	// Destructor
	void OnDestroy()
	{
		audio.Stop();
		PlayAmbientMusic.instance = null;
	}
	
	private void StartPlayingMusic()
	{
		audio.Stop();
		if(GravityController.GravityPolarity > 0)
		{
			audio.clip = downWorldMusic;
		}
		else if(GravityController.GravityPolarity < 0)
		{
			audio.clip = upWorldMusic;
		}

		audio.Play();
	}

	private static bool DoesExist()
	{
		if(PlayAmbientMusic.instance == null)
		{
			Debug.LogError("PlayAmbientMusic and MusicPlayer do not exist!");
			return false;
		}

		return true;
	}

	public static void ReplayCorrectMusic()
	{
		if(PlayAmbientMusic.DoesExist() == false) return;

		PlayAmbientMusic.instance.StartPlayingMusic();
	}
}
