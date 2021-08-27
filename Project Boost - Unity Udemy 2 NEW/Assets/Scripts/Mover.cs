
using UnityEngine;

public class Mover : MonoBehaviour
{   
    //Parameters
    [SerializeField] float thrust;
    [SerializeField] float rotatePower;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainBoosterParticle;
    [SerializeField] ParticleSystem leftBoosterParticle;
    [SerializeField] ParticleSystem rightBoosterParticle;
    //Cache
    Rigidbody rb;
    AudioSource audioS;

    //State
    

    // Start is called before the first frame update
    void Start()
    {



        Debug.Log("THIS IS THE MAIN BRANCH");

        rb = GetComponent<Rigidbody>();
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ThrustUp();
        RotationControl();
    }
    void ThrustUp()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void RotationControl()
    {
        // Right Rotate
        if (Input.GetKey(KeyCode.D))
        {
            RotateLeft();
        }
        // Left Rotate
        else if (Input.GetKey(KeyCode.A))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }
    private void RotateLeft()
    {
        if (!rightBoosterParticle.isPlaying)
        {
            rightBoosterParticle.Play();
        }
        ApplyRotation(-rotatePower);
    }
    private void RotateRight()
    {
        if (!leftBoosterParticle.isPlaying)
        {
            leftBoosterParticle.Play();
            
        }
        ApplyRotation(rotatePower);
    }

    private void StopRotating()
    {
        leftBoosterParticle.Stop();
        rightBoosterParticle.Stop();
    }

    void ApplyRotation(float rotation)
    {   
        rb.freezeRotation = true; 
        transform.Rotate(Vector3.forward * Time.deltaTime * rotation);
        rb.freezeRotation = false;
    }
    
    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * Time.deltaTime * thrust);
        if (!audioS.isPlaying)
        {
            audioS.PlayOneShot(mainEngine);
        }
        if (!mainBoosterParticle.isPlaying)
        {
            mainBoosterParticle.Play();
        }
    }
    private void StopThrusting()
    {
        audioS.Stop();
        mainBoosterParticle.Stop();
    }
}
