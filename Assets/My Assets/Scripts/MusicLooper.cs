using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class MusicLooper : MonoBehaviour {

	//Will store all ambient music clips.  Start () will load every music file in the file PathName.
	public List<FileInfo> musicFiles = new List<FileInfo> ();
	//The audio source that will play music.  This script will load random music clips into the source at run time.
	public AudioSource defaultSource;

	//We don't want to repeat songs; store the previous song's index into musicFiles here.
	private int previousIndex = -1;
	//Path to the file music files will be in.  Due to how Unity loads files, songs MUST be somewhere in Assets/Resources.
	private readonly string pathName = "Assets/Resources/LevelAmbientMusic";
	//Resoures.Load() assumes its path its relative to the Resources directory.  Put that path here.
	private readonly string pathInResources = "LevelAmbientMusic/";

	/*
	 * If you want to use non-.mp3 music files, add the string "*.xxx" to this array, where .xxx is the
	 * extension you want.
	 */
	private readonly string[] fileExtensions = {"*.mp3"};


	//Load the path to each music file into musicFiles.
	void Start () {
		DirectoryInfo musicDir = new DirectoryInfo (pathName);
		foreach (string extension in fileExtensions) {
			foreach (FileInfo fi in musicDir.GetFiles (extension)) {
				musicFiles.Add (fi);
			}
		}
	}

	//Take action if no music is playing.
	void Update () {
		if (!defaultSource.isPlaying) { //If the music stopped, randomly choose a new clip.
			//But first, unload the current clip
			Resources.UnloadAsset(defaultSource.clip);
			int index;
			do {
				index = Random.Range (0, musicFiles.Count);
			} while (index == previousIndex);
			previousIndex = index;
			string path = pathInResources + Path.GetFileNameWithoutExtension(musicFiles[index].ToString ());
			//Resources.Load expects a path relative to the Resources directory, that has no extension.
			defaultSource.clip = Resources.Load(path) as AudioClip;
			defaultSource.Play ();
		}
	}
}
