
using UnityEngine;
using TMPro;

public class DeathCounter : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    float deaths;

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
