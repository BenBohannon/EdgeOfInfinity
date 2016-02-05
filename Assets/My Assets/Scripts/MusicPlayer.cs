using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	
	void Awake() {
//		Debug.Log ("music player AWAKE " + GetInstanceID());
		if (instance != null) {
			Destroy (gameObject);
			print ("duplicate music player: self-destruct!");
		} else {
			instance = this;
		}
		GameObject.DontDestroyOnLoad(gameObject);
	}
	// Use this for initialization
	void Start () {
//		Debug.Log ("music player START " + GetInstanceID());
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
