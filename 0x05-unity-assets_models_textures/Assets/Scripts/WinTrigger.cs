using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public GameObject TextObj;
    public GameObject PlayerObj;

    private Text text;
    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        text = TextObj.GetComponent<Text>();
        timer = PlayerObj.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        timer.enabled = false;
        text.fontSize = 60;
        text.color = Color.green;
    }
}
