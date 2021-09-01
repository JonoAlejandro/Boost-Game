using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

//POG
public class CollisionH : MonoBehaviour
{

    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] AudioSource audioWin;
    [SerializeField] AudioSource audioDie;
    [SerializeField] float delay = 1f;
    [SerializeField] CheatsText cheatsText; 

    public AudioMixerSnapshot unpaused;
    public AudioMixerSnapshot victory;
    public float victorySnapshotTransition;
    public float unpausedSnapshotTransition;


    Vector3 startPlayerPos;
    Quaternion startPlayerRotation;

    Rigidbody rb;
    Mover moveComponent;
    ParticleSystem particleSys;

    bool isTransitioning = false;
    bool collisionDisable = false;
    bool DisableMovement = false;
    bool won = false;
    bool cheatsToggle = false;

    public bool Won { get { return won; } }

    void Start()
    {
        
        SavingPlayerPosition();
        GrabComponent();
    }

    void GrabComponent()
    {
        cheatsText = FindObjectOfType<CheatsText>(); 
        rb = GetComponent<Rigidbody>();
        
        moveComponent = GetComponent<Mover>();
        particleSys = GetComponent<ParticleSystem>();
    }

    void SavingPlayerPosition()
    {
        startPlayerPos = transform.position;
        startPlayerRotation = transform.rotation;

    }

    void Update()
    {
        RespondToDebugKeys();
        ReloadOnPress();
        DisabledPlayerMovement();
    }

    void DisabledPlayerMovement()
    {
        if (DisableMovement)
        {
            rb.isKinematic = false;
            DisableMovement = true;
        }
    }

    void ReloadOnPress()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // If Paused Check is false then reload Level.
            if (!IsPausedCheck())
            {
                ReloadLevel();
            }
        }
    }
    bool IsPausedCheck()
    {   
        Pause pause = FindObjectOfType<Pause>();
        if (pause.PauseToggle)
        {
            return true;
        }
        return false;

    }
    void RespondToDebugKeys()
    {
        //Enable Cheats
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TogglingCheatsOnOrOff();
        }

        if (!cheatsToggle) { return; }

        cheatsText.Activate();
        
        //Next Level
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadLevel(true);
        }
        //Previous Level 
        else if (Input.GetKeyDown(KeyCode.J))
        {
            LoadLevel(false);
        }
        //toggle collision
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable; 
            Debug.Log("Disabled Collision");
        }
    }

void TogglingCheatsOnOrOff()
    {
        cheatsToggle = !cheatsToggle;
        Debug.Log("Toggled Cheats " + cheatsToggle);
        cheatsText.Deactivate();
    }

    void OnTriggerEnter(Collider other)
    {
        if (isTransitioning || collisionDisable) { return; }

        switch (other.gameObject.tag)
        {
            case "Invisible":
                StartCrashSequence();
                break;
            default:
                break;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisable) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                Debug.Log("You're Winner\n" + "Loading next level");
                VictorySnapshotTransition();
                StartSuccessSequence();

                break;
            default:
                Debug.Log("You Died");
                StartCrashSequence();
                break;
        }
    }

    void VictorySnapshotTransition()
    {
        victory.TransitionTo(victorySnapshotTransition);


    }

    void StartSuccessSequence()
    {
        if (cheatsToggle)
        {
            TogglingCheatsOnOrOff();
        }

        successParticle.Play();
        moveComponent.enabled = false;
        isTransitioning = true;


        audioWin.Play();
        won = true;



        StartCoroutine(WaitBeforeShow());
    }
    void StartCrashSequence()
    {
        explosionParticle.Play();
        moveComponent.enabled = false;
        isTransitioning = true;
        
        audioDie.Play();
    }

    IEnumerator WaitBeforeShow()
    {
        yield return new WaitForSeconds(delay);
        unpaused.TransitionTo(unpausedSnapshotTransition);
        LoadLevel(true);
    }

    void LoadLevel(bool nextIsTrue)
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int SceneIndex;
        

        switch (nextIsTrue)
        {
            // Loading Next
            case true:
                Debug.Log("Loading Next Level");
                SceneIndex = currentSceneIndex + 1;
                if (SceneIndex == SceneManager.sceneCountInBuildSettings)
                {
                    SceneIndex = 0;
                }
                SceneManager.LoadScene(SceneIndex);

                break;
            // Loading Previous
            case false:

                if (currentSceneIndex != 0)
                {
                    Debug.Log("Loading Previous Level");
                    SceneIndex = currentSceneIndex - 1;
                    SceneManager.LoadScene(SceneIndex);
                }
                break;
        }

    }
    void ReloadLevel()
    {
        if (won) { return; }

        audioDie.Stop();
        transform.position = startPlayerPos;
        transform.rotation = startPlayerRotation;
        moveComponent.enabled = true;
        isTransitioning = false;
        rb.isKinematic = true;
        DisableMovement = true;

    }
}
