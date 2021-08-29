using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject controlsMenu;
    // Components

    //States
    bool pauseToggle = false;


    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        PauseGame();
        FirstSceneCheck();
    }

    void FirstSceneCheck()
    {
        //checking if current scene is main menu
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene == 0)
        {
            Destroy(gameObject);
            Debug.Log("deleted Pause Screen");
            Time.timeScale = 1;
        }
    }

    void PauseGame()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseToggle = !pauseToggle;
            Debug.Log("Pause is " + pauseToggle);
        }

        
        if (Input.GetKeyDown(KeyCode.Escape) && pauseToggle)
        {
            PauseSequence();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseToggle)
        {
            UnpauseSequence();
        }

    }

    void PauseSequence()
    {
        Time.timeScale = 0;
        Debug.Log("Going to Pause Menu");
        Cursor.visible = true;
        mainMenu.SetActive(true);
    }

    public void UnpauseSequence()
    {
        controlsMenu.SetActive(false);
        mainMenu.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("Unpausing Game");
        Cursor.visible = false; 
    }

    public void GoToMainMenu()
    {   
        SceneManager.LoadScene(0);
    }
}
