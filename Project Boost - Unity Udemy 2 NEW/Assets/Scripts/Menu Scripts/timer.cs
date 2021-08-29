
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeStart;

    private void Start()
    {
       timerText.text = timeStart.ToString("F2"); 
    }

    // Update is called once per frame
    void Update()
    {
        timeStart += Time.deltaTime;
        timerText.text = "Timer: " + timeStart.ToString("F2");
    }
}
