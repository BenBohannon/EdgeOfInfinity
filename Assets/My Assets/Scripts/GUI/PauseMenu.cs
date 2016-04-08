using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public GameObject optionsMenuObject;

    public void optionsClick()
    {
        optionsMenuObject.SetActive(true);
    }

    public void controlsClick()
    {

    }

    public void exitClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}
