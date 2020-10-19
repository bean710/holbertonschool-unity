using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject Player;
    public GameObject TimerCanvas;

    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        pc = Player.GetComponent<PlayerController>();
    }

    void AnimDone()
    {
        MainCamera.SetActive(true);
        TimerCanvas.SetActive(true);
        pc.enabled = true;
        transform.gameObject.SetActive(false);
    }
}
