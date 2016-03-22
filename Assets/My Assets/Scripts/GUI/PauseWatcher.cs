using UnityEngine;
using System.Collections;

public class PauseWatcher : MonoBehaviour {

	public string pauseButton = "p";
	public bool isPaused = false;
	public Canvas pauseScreen;

	// Use this for initialization
	void Start () {
		pauseScreen.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (pauseButton)) {
			togglePaused ();
		}
	}

	public void togglePaused () {
		isPaused = !isPaused;
		pauseScreen.enabled = !pauseScreen.enabled;
		if (isPaused) {
			Time.timeScale = 0; //Stop time
		} else {
			Time.timeScale = 1; //Go back to "normal" speed
		}
	}
}
