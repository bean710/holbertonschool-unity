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

        BGMSlider.value = DBToLinear(PlayerPrefs.GetFloat("BGMVolume"));
        SFXSlider.value = DBToLinear(PlayerPrefs.GetFloat("SFXVolume"));

        /**
        BGMSlider.onValueChanged.AddListener(delegate {ChangeBGMVolume(); });
        SFXSlider.onValueChanged.AddListener(delegate {ChangeSFXVolume(); });
        **/
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
        ChangeBGMVolume();
        ChangeSFXVolume();
    }

    public void ChangeBGMVolume()
    {
        float val = BGMSlider.value;
        float valDB = LinearToDB(val);
        mixer.SetFloat("BGMVolume", valDB);
        PlayerPrefs.SetFloat("BGMVolume", valDB);
    }

    public void ChangeSFXVolume()
    {
        float val = SFXSlider.value;
        float valDB = LinearToDB(val);
        mixer.SetFloat("SFXVolume", valDB);
        PlayerPrefs.SetFloat("SFXVolume", valDB);
    }

    private float LinearToDB(float linear)
    {
        if (linear == 0)
            return (-144.0f);

        return (20.0f * Mathf.Log10(linear));
    }

    private float DBToLinear(float db)
    {
        return Mathf.Pow(10.0f, db/20.0f);
    }
}
