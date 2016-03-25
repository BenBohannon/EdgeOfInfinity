using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    public List<string> worldOneNames;
    public List<string> worldTwoNames;
    public List<string> worldThreeeNames;
    public List<string> worldFourNames;
    public List<string> worldFiveNames;
    public List<string> worldSixNames;
    public List<string> worldSevenNames;

    public GameObject mainMenuObject;

    private List<List<string>> worldList;

    private int worldIndex = 0;
    private int levelIndex = 0;

    public UnityEngine.UI.Text mainPanelText;

    void Awake()
    {
        worldList = new List<List<string>>(7);
        worldList.Add(worldOneNames);
        worldList.Add(worldTwoNames);
        worldList.Add(worldThreeeNames);
        worldList.Add(worldFourNames);
        worldList.Add(worldFiveNames);
        worldList.Add(worldSixNames);
        worldList.Add(worldSevenNames);

        mainPanelText.text = worldList[worldIndex][levelIndex];
    }

    void OnEnable()
    {
        worldIndex = 0;
        levelIndex = 0;

        mainPanelText.text = worldList[worldIndex][levelIndex];
    }
    
    public void switchWorld(int i)
    {
        worldIndex = i - 1;
        levelIndex = 0;

        mainPanelText.text = worldList[worldIndex][levelIndex];
    }

    public void incrementLevel()
    {
        if ((levelIndex + 1) < worldList[worldIndex].Count)
        {
            levelIndex++;
        }

        mainPanelText.text = worldList[worldIndex][levelIndex];
    }

    public void decrementLevel()
    {
        if ((levelIndex - 1) >= 0)
        {
            levelIndex--;
        }

        mainPanelText.text = worldList[worldIndex][levelIndex];
    }

    public void StartLevel()
    {
        int sceneIndex = 0;
        for (int i = 0; i < worldIndex; i++)
        {
            sceneIndex += worldList[i].Count;
        }

        SceneManager.LoadScene(sceneIndex + levelIndex + 1);
    }

    public void backButtonClick()
    {
        mainMenuObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
	
}
