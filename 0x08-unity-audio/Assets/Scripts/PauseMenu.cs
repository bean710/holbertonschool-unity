using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public AudioMixer mixer;

    private bool paused = false;
    private PlayerController pc;

    private float myTime = 0.0F;
    private float nextIn = 0.5F;

    private Timer t;

    void Start()
    {
        pc = GetComponent<PlayerController>();
        Debug.Log("Loaded");

        t = GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        myTime += Time.deltaTime;

        if (Input.GetButton("Pause") && myTime > nextIn)
        {
            nextIn = myTime + 0.5F;

            if (!paused)
                Pause();
            else
                Resume();
        }
    }
    
    public void Pause()
    {
        paused = true;
        pauseCanvas.SetActive(true);
        pc.TogglePause();
        t.ToggleState();
        float curVal;
        mixer.GetFloat("BGMVolume", out curVal);
        mixer.SetFloat("BGMVolume", curVal - 15f);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        paused = false;
        pauseCanvas.SetActive(false);
        pc.TogglePause();
        t.ToggleState();
        float curVal;
        mixer.GetFloat("BGMVolume", out curVal);
        mixer.SetFloat("BGMVolume", curVal + 15f);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Restart()
    {
        SceneChanger.ChangeScene(SceneChanger.CurrentScene());
    }

    public void MainMenu()
    {
        SceneChanger.ChangeScene("MainMenu");
    }

    public void Options()
    {
        SceneChanger.ChangeScene("Options");
    }
}
