using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public GameObject levelSelectObject;
    public GameObject optionsMenuObject;

	
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
        optionsMenuObject.SetActive(true);
    }

    public void exitButtonClick()
    {
        Application.Quit();
    }
}
