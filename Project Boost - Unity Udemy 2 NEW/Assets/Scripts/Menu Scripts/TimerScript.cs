
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    float timeStartSeconds = 0f;
    public float TimeStartSeconds { get { return timeStartSeconds; } }
   


    private void Start()
    {
       timerText.text = timeStartSeconds.ToString("F2"); 
    }

    // Update is called once per frame
    void Update()
    {


        timeStartSeconds += Time.deltaTime;

        timerText.text = "Time:  " + timeStartSeconds.ToString("F2");
    }
}


