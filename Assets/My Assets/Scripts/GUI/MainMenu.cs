using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public GameObject levelSelectObject;

	
	public void startButtonClick()
    {
        levelSelectObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void creditsButtonClick()
    {

    }

    public void optionsButtonClick()
    {

    }

    public void exitButtonClick()
    {
        Application.Quit();
    }
}
