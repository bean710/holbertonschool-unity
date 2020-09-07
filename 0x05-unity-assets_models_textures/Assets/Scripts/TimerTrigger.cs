using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    public GameObject TimerText;

    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = TimerText.GetComponent<Timer>();
    }

    private void OnTriggerExit(Collider other) {
        timer.enabled = true;
    }
}
