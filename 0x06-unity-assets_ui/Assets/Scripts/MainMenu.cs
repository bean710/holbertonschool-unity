using System.Collections;
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
    }

    public void Options()
    {
        SceneChanger.ChangeScene("Options");
    }
}
