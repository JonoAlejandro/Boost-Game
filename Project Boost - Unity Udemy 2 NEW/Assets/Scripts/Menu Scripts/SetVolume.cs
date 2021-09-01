using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{

    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("Music Volume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX Volume", 1f);
    }

    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("Music Volume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("Music Volume", sliderValue);
    }
    public void SetSFXLevel(float sliderValue)
    {
        mixer.SetFloat("SFX Volume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFX Volume", sliderValue);
    }
}