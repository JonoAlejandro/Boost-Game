
using UnityEngine;
using TMPro;

public class DeathCounter : MonoBehaviour
{
    TextMeshProUGUI timerText;
    float deaths = 0;

    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }


    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.R))
        {   
            DeathCheck();
        }
        timerText.text = "Deaths: " + deaths;
    }

    private void DeathCheck()
    {
        CollisionH collisionH = FindObjectOfType<CollisionH>();
        Pause pause = FindObjectOfType<Pause>();

        // Checking so death counter goes up in pause menu and on win.
        if (!pause.PauseToggle && !collisionH.Won)
        {
            Debug.Log("pause.Toggle is " + pause.PauseToggle);
            deaths++;
        }
    }


}
