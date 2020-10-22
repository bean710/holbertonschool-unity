using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public List<string> levelNames;
    public AudioMixer mixer;

    private void Start() {
        mixer.SetFloat("BGMVolume", PlayerPrefs.GetFloat("BGMVolume"));
        mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
    }

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
