using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;


public class Pause : MonoBehaviour
{

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject controlsButton;
    [SerializeField] GameObject controlsMenu;
    [SerializeField] GameObject VictoryScreen;
    [SerializeField] GameObject OptionsMenu;

    // Components
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timerTextVictory;
    public TextMeshProUGUI deathText;
    public TextMeshProUGUI deathTextVictory;
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;
    public AudioSource backgroundMusic;
    public float pauseMusicTransitionTime;
    //States
    bool pauseToggle = false;
    bool isOnVictoryScreen = false;


    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        PauseGame();
        Lowpass();
        FirstSceneCheck();
        VictoryScreenCheck();

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

    void Lowpass()
    {
        
        if (Time.timeScale == 0)
        {            
            paused.TransitionTo(pauseMusicTransitionTime);
            AudioListener.pause = true;
            backgroundMusic.ignoreListenerPause = true;
        }
        else if (Time.timeScale == 1)
        {
            unpaused.TransitionTo(pauseMusicTransitionTime);
            AudioListener.pause = false;
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
        OptionsMenu.SetActive(false);

        Time.timeScale = 1;
        Debug.Log("Unpausing Game");
        Cursor.visible = false; 
    }

    public void GoToMainMenu()
    {
        unpaused.TransitionTo(pauseMusicTransitionTime);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        AudioListener.pause = false;
    }

    void VictoryScreenCheck()
    {
        // Checking if Scene is the last
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int SceneIndex = currentSceneIndex + 1;
       
     
        if (SceneIndex == SceneManager.sceneCountInBuildSettings && !isOnVictoryScreen)
        {
            
            Debug.Log("this is the last level");
 
            timerTextVictory.text = timerText.text;
            deathTextVictory.text = deathText.text;

            // changing states
            VictoryScreen.SetActive(true);
            timerText.enabled = false;
            deathText.enabled = false;
            mainMenu.SetActive(true);
            playButton.SetActive(false);
            controlsButton.SetActive(false);
            isOnVictoryScreen = true;
        }
    }
}
