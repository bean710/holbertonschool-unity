using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject TimerText;
    public GameObject FinalTime;
    public GameObject WinCanvas;

    private Stopwatch sw;
    private Text text;
    private Text ftText;
    private CameraController CC;

    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        sw = new Stopwatch();

        text = TimerText.GetComponent<Text>();
        ftText = FinalTime.GetComponent<Text>();

        CC = GameObject.Find("Main Camera").GetComponent<CameraController>();

        sw.Start();
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan ts = sw.Elapsed;
        text.text = string.Format("{0:00}:{1:00}.{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
    }

    public void ToggleState()
    {
        if (sw == null)
            return;

        if (paused)
        {
            paused = false;
            sw.Start();
        }
        else
        {
            paused = true;
            sw.Stop();
        }
    }

    public void Win()
    {
        CC.TogglePause();
        WinCanvas.SetActive(true);
        ftText.text = text.text;
        text.fontSize = 60;
        text.color = Color.green;
        text.text = "";
    }
}
