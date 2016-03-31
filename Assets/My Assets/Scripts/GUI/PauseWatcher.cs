using UnityEngine;
using System.Collections;

public class PauseWatcher : MonoBehaviour {

	public string pauseButton = "p";
	public GameObject pauseScreen;
    private static PauseWatcher singleton;

    private bool isPaused
    {
        get
        {
            return MasterDriver.isPaused;
        }
        set
        {
            MasterDriver.isPaused = value;
        }
    }

	// Use this for initialization
	void Start () {
        //Only allow one PauseWatcher
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this);
        }

		pauseScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (pauseButton)) {
			togglePaused ();
		}
	}

	public void togglePaused () {
		isPaused = !isPaused;
        pauseScreen.SetActive(isPaused);
        if (isPaused)
        {
            Time.timeScale = 0; //Stop time
        }
        else
        {
            Time.timeScale = 1; //Go back to "normal" speed
        }
	}
}
