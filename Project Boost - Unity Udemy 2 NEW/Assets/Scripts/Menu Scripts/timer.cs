
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    float timeStart = 0f;
    public float TimeStart { get { return timeStart; } }

   

    private void Start()
    {
       timerText.text = timeStart.ToString("F2"); 
    }

    // Update is called once per frame
    void Update()
    {
        timeStart += Time.deltaTime;
        timerText.text = "Time: " + timeStart.ToString("F2");
    }
}
