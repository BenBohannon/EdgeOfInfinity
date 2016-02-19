﻿using UnityEngine;
using System.Collections.Generic;

public class LevelDriver : MonoBehaviour {

    public int charactersToSave = 1;
    public CharacterMove[] requiredCharacters;

    private int savedCharacters = 0;
    private List<CharacterMove> requiredList;

    [HideInInspector]
    public float levelTime = 0;

    void Awake()
    {
        //Overwrite the last levelDriver with this one.
        if (MasterDriver.levelDriver != null)
        {
            Destroy(MasterDriver.levelDriver);
        }

        MasterDriver.levelDriver = this;
    }

    void Start()
    {
        //Copy the required characters into a list, so it's easier to remove them.
        requiredList = new List<CharacterMove>(requiredCharacters.Length);
        foreach(CharacterMove c in requiredCharacters)
        {
            requiredList.Add(c);
        }
    }

    void Update()
    {
        levelTime += Time.deltaTime;
    }

	public void saveCharacter(CharacterMove character)
    {
        savedCharacters++;
        requiredList.Remove(character);

        if (requiredList.Count == 0 && savedCharacters == charactersToSave)
        {
            EndLevel();
        }
    }

    private void EndLevel()
    {
        //TODO: Show some success screen or menu that tells the player how they've done.


    }
}
