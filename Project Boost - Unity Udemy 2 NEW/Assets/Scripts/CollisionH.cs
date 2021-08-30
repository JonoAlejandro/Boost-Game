using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

//POG
public class CollisionH : MonoBehaviour
{

    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] AudioSource audioWin;
    [SerializeField] AudioSource audioDie;
    [SerializeField] float delay = 1f;

    Vector3 startPlayerPos;
    Quaternion startPlayerRotation;

    Rigidbody rb;
    Mover moveComponent;
    ParticleSystem particleSys;

    bool isTransitioning = false;
    bool collisionDisable = false;
    bool DisableMovement = false;
    bool won = false;

    void Start()
    { 
        SavingPlayerPosition();
        GrabComponent();
    }

    void GrabComponent()
    {
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
