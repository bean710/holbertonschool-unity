using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public GameObject YToggle;
    public AudioMixer mixer;

    public Slider BGMSlider;
    public Slider SFXSlider;

    private Toggle yToggleElement;

    private bool yInvert = false;

    void Start()
    {
        yToggleElement = YToggle.GetComponent<Toggle>();
        yToggleElement.isOn = (PlayerPrefs.GetInt("yInvert", 0) == 1 ? true : false);

        BGMSlider.onValueChanged.AddListener(delegate {ChangeBGMVolume(); });
        SFXSlider.onValueChanged.AddListener(delegate {ChangeSFXVolume(); });
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

    public void ChangeBGMVolume()
    {
        float val = BGMSlider.value;
        mixer.SetFloat("BGMVolume", LinearToDB(val));
    }

    public void ChangeSFXVolume()
    {
        float val = SFXSlider.value;
        mixer.SetFloat("SFXVolume", LinearToDB(val));
    }

    private float LinearToDB(float linear)
    {
        if (linear == 0)
            return (-144.0f);

        return (20.0f * Mathf.Log10(linear));
    }
}
