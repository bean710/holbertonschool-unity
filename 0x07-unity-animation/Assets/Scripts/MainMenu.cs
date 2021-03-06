﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public List<string> levelNames;

    private SceneChanger sc;
    public void LevelSelect(int level)
    {
        SceneChanger.ChangeScene(levelNames[level - 1]);
        Debug.Log("Changing level");
    }

    public void Options()
    {
        SceneChanger.ChangeScene("Options");
    }

    public void Quit()
    {
        Debug.Log("Exited");
        Application.Quit();
    }
}
