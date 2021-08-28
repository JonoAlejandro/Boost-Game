using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    float time;


    // Update is called once per frame
    void Update()
    {
        time = Time.time;
        int timeInt = (int)time;
        timerText.text = "Timer: " + timeInt;

    }
}
