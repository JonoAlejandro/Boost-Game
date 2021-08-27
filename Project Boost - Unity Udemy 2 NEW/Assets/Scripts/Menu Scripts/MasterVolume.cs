using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolume : MonoBehaviour
{   
    //TODO FINISH THIS
    float volume;
    Slider volumeSlider;
    [SerializeField] 
    // Start is called before the first frame update
    void Start()
    {
        volumeSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        volume = volumeSlider.value;
    }
}
