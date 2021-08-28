
using UnityEngine;

public class Mover : MonoBehaviour
{   
    //Parameters
    [SerializeField] float rotatePower;

    [SerializeField] float thrust;
    [SerializeField] ParticleSystem mainBoosterParticle;
    [SerializeField] ParticleSystem leftBoosterParticle;
    [SerializeField] ParticleSystem rightBoosterParticle;
    [SerializeField] AudioClip mainEngine;
  
    [SerializeField] [Range(0, 1)] float mainThrustVolume;
    //Cache
    Rigidbody rb;
    AudioSource audioS;

    //State
    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("THIS IS THE PROTOTYPE BRANCH");
        Cursor.visible = false;
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
            RotateRight();
        }
        else
        {
            rightBoosterParticle.Stop();         
        }
        // Left Rotate
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else
        {
            leftBoosterParticle.Stop();
        }
    }
    private void RotateRight()
    {
        if (!rightBoosterParticle.isPlaying)
        {
          
            rightBoosterParticle.Play();
        }
        ApplyRotation(-rotatePower);
    }
    private void RotateLeft()
    {
        if (!leftBoosterParticle.isPlaying)
        {
   
            leftBoosterParticle.Play();
            
        }
        ApplyRotation(rotatePower);
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
            audioS.PlayOneShot(mainEngine, mainThrustVolume);
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
