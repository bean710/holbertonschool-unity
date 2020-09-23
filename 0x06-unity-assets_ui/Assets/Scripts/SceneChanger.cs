using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private static string lastScene;
    private static string currentScene = "MainMenu";

    void Awake()
    {
        DontDestroyOnLoad(this.transform.gameObject);
    }

    public static void ChangeScene(string sceneName)
    {
        lastScene = currentScene;
        currentScene = sceneName;
        SceneManager.LoadScene(currentScene);
    }

    public static void LastScene()
    {
        string swap = currentScene;
        currentScene = lastScene;
        lastScene = swap;
        SceneManager.LoadScene(currentScene);
    }
}
