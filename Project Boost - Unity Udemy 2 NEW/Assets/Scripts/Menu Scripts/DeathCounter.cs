using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathCounter : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    float deaths;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.R))
        {
            deaths++;
        }

        timerText.text = "Deaths: " + deaths;
    }
}
