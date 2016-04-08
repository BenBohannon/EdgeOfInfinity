using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class MusicLooper : MonoBehaviour {

	//The audio source that will play music.  This script will load random music clips into the source at run time.
	public AudioSource defaultSource;

    public List<AudioClip> audioClips = new List<AudioClip>();

	//We don't want to repeat songs; store the previous song's index into musicFiles here.
	private int previousIndex = -1;
	//Path to the file music files will be in.  Due to how Unity loads files, songs MUST be somewhere in Assets/Resources.
	private readonly string pathName = "LevelAmbientMusic";
	//Resoures.Load() assumes its path its relative to the Resources directory.  Put that path here.
	private readonly string pathInResources = "LevelAmbientMusic/";

	/*
	 * If you want to use non-.mp3 music files, add the string "*.xxx" to this array, where .xxx is the
	 * extension you want.
	 */
	private readonly string[] fileExtensions = {"*.mp3", "*.wav"};


	//Load the path to each music file into musicFiles.
	void Start () {
        foreach(AudioClip o in Resources.LoadAll<AudioClip>(pathName))
        {
            audioClips.Add(o);
        }
	}

	//Take action if no music is playing.
	void Update () {
		if (!defaultSource.isPlaying) { //If the music stopped, randomly choose a new clip.
			int index;
			do {
				index = Random.Range (0, audioClips.Count);
			} while (index == previousIndex && audioClips.Count > 1);
			previousIndex = index;
			//Resources.Load expects a path relative to the Resources directory, that has no extension.
            defaultSource.clip = audioClips[index];
			defaultSource.Play ();
		}
	}
}
