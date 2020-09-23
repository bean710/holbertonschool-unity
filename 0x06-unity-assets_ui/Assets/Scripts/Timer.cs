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

    private Stopwatch sw;
    private Text text;

    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        sw = new Stopwatch();

        text = TimerText.GetComponent<Text>();

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
}
