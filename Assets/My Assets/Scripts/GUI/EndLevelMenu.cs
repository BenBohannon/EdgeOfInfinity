using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndLevelMenu : MonoBehaviour {

    public Text timeText;
    public Text parTimeText;
    public Text savedText;

    public void setupMenu(float time, int parTime, int saved)
    {
        timeText.text = timeText.text + secondsToTime(time);
        parTimeText.text = parTimeText.text + secondsToTime(parTime);
        savedText.text = savedText.text + saved.ToString();
    }

    private string secondsToTime(float seconds)
    {
        return (((int)seconds)/60) + ":" + (((int)seconds)%60);
    }

    public void MainMenuClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void nextLevelClick()
    {
        int sceneNum = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        if (sceneNum < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNum + 1);
        }
        else
        {
            MainMenuClick();
        }
    }

}
