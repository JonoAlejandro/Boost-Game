using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour
{
    timer timerCounter;
    DeathCounter  deaths;

    [SerializeField] TextMeshProUGUI finalTimeHS;
    [SerializeField] TextMeshProUGUI deathsHS;

    string hsTimer;
    string hsDeaths;

    float timerSave;
    int deathSave;

    bool victoryScreen = false;

    private void Start()
    {
        timerCounter = FindObjectOfType<timer>();
        deaths = FindObjectOfType<DeathCounter>();
        hsTimer = "Time: ";
        hsDeaths = "Deaths: ";

    }

    int CheckFinalScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int SceneIndex;
        SceneIndex = currentSceneIndex + 1;
        return SceneIndex;
    }

    // Update is called once per frame
    void Update()
    {
       int SceneIndex = CheckFinalScene();
        

        //Checking if we are in last level.
        if (SceneIndex == SceneManager.sceneCountInBuildSettings && !victoryScreen)
        {
            Cursor.visible = true;
            victoryScreen = true;
            Debug.Log(timerCounter.TimeStart);


            //Setting Old Scores
            SettingText();

            //timer and Death save to PlayerPrefs if beat highscore
            HighScoreConditions();

        }



    }

    private void SettingText()
    {
        if (PlayerPrefs.GetFloat("Timer HighScore") == 9999)
        {
            finalTimeHS.text = hsTimer + "XXXX";
            deathsHS.text = hsDeaths + "XXXX";
        }
        else
        {
            finalTimeHS.text = hsTimer + PlayerPrefs.GetFloat("Timer HighScore").ToString("F2");
            deathsHS.text = hsDeaths + PlayerPrefs.GetInt("Deaths HighScore");
        }
    }

    private void HighScoreConditions()
    {
        if (PlayerPrefs.GetFloat("Timer HighScore") > timerCounter.TimeStart)
        {
            timerSave = timerCounter.TimeStart;
            finalTimeHS.text = hsTimer + timerSave.ToString("F2");
            PlayerPrefs.SetFloat("Timer HighScore", timerSave);
        }

        if (PlayerPrefs.GetInt("Deaths HighScore") > deaths.Deaths)
        {
            deathSave = deaths.Deaths;
            deathsHS.text = hsDeaths + deathSave;
            PlayerPrefs.SetInt("Deaths HighScore", deathSave);
        }
    }


    public void Reset()
    {
        int resetNum = 9999;
        PlayerPrefs.SetInt("Deaths HighScore", resetNum);
        PlayerPrefs.SetFloat("Timer HighScore", resetNum);
        SettingText();

    }
}
