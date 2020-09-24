using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject YToggle;

    private Toggle yToggleElement;

    private bool yInvert = false;

    void Start()
    {
        yToggleElement = YToggle.GetComponent<Toggle>();
        yToggleElement.isOn = (PlayerPrefs.GetInt("yInvert", 0) == 1 ? true : false);
    }

    public void Back()
    {
        SceneChanger.LastScene();
    }

    public void SetYInvert()
    {
        yInvert = yToggleElement.isOn;
    }
    
    public void Apply()
    {
        PlayerPrefs.SetInt("yInvert", (yInvert ? 1 : 0));
    }
}
