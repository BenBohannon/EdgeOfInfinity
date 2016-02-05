using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel (string levelName) {
		Debug.Log ("level load requested for level: " + levelName);
		Application.LoadLevel(levelName);
	}
	
	public void QuitRequest() {
		Debug.Log ("quit requested");
		Application.Quit();
	}
}
