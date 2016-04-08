using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public GameObject levelSelectObject;
    public GameObject optionsMenuObject;
    public GameObject creditsMenuObject;

	
	public void startButtonClick()
    {
        levelSelectObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void creditsButtonClick()
    {
        creditsMenuObject.SetActive(true);
    }

    public void optionsButtonClick()
    {
        optionsMenuObject.SetActive(true);
    }

    public void exitButtonClick()
    {
        Application.Quit();
    }
}
