using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public GameObject PlayerObj;

    public AudioSource backgroundMusic;
    public AudioSource winMusic;

    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = PlayerObj.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (backgroundMusic.isPlaying)
            backgroundMusic.Stop();
        winMusic.Play();

        Cursor.lockState = CursorLockMode.None;
        timer.enabled = false;
        timer.Win();
    }
}
