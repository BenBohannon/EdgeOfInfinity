using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public GameObject optionsMenuObject;
    public GameObject controlsObject;

    public void optionsClick()
    {
        optionsMenuObject.SetActive(true);
    }

    public void controlsClick()
    {
        controlsObject.SetActive(true);
    }

    public void controlsClose()
    {
        controlsObject.SetActive(false);
    }

    public void exitClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}
