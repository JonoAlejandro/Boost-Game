using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;


public class CollisionH : MonoBehaviour
{
    [SerializeField] AudioClip win;
    [SerializeField] AudioClip death;
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] [Range(0, 1)] float victoryVolume;
    [SerializeField] float delay = 1f;
    Rigidbody rb;

    Vector3 startPlayerPos;
    Quaternion startPlayerRotation;

    AudioSource audioS;
    Mover moveComponent;
    ParticleSystem particleSys;

    bool isTransitioning = false;
    bool collisionDisable = false;
    bool disabled = false;


    void Start()
    { 
        SavingPlayerPosition();
        GrabComponent();
    }

    void GrabComponent()
    {
        rb = GetComponent<Rigidbody>();
        audioS = GetComponent<AudioSource>();
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
        if (disabled)
        {
            rb.isKinematic = false;
            disabled = true;
        }
    }

    void ReloadOnPress()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    void RespondToDebugKeys()
    {
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
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable; //toggle collision
            Debug.Log("Disabled Collision");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
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
                StartSuccessSequence();
                break;
            default:
                Debug.Log("You Died");
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        successParticle.Play();
        moveComponent.enabled = false;
        isTransitioning = true;
        audioS.Stop();
        audioS.PlayOneShot(win, victoryVolume);

        StartCoroutine(WaitBeforeShow());
    }
    void StartCrashSequence()
    {
        explosionParticle.Play();
        moveComponent.enabled = false;
        isTransitioning = true;
        audioS.Stop();
        audioS.PlayOneShot(death);
    }

    IEnumerator WaitBeforeShow()
    {
        yield return new WaitForSeconds(delay);
        LoadLevel(true);
    }

    void LoadLevel(bool nextIsTrue)
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int SceneIndex;
        Debug.Log("This is Level: " + currentSceneIndex);

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
        transform.position = startPlayerPos;
        transform.rotation = startPlayerRotation;
        moveComponent.enabled = true;
        isTransitioning = false;
        rb.isKinematic = true;
        disabled = true;


        //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(currentSceneIndex);
    }
}
