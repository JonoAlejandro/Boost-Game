using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{

    public AudioMixer mixer;

    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("Music Volume", Mathf.Log10(sliderValue) * 20);
    }
    public void SetSFXLevel(float sliderValue)
    {
        mixer.SetFloat("SFX Volume", Mathf.Log10(sliderValue) * 20);
    }
}