
using UnityEngine;

public class Mover : MonoBehaviour
{   
    //Parameters
    [SerializeField] float rotatePower;

    [SerializeField] float thrust;

    [SerializeField] ParticleSystem mainBoosterParticle;
    [SerializeField] ParticleSystem leftBoosterParticle;
    [SerializeField] ParticleSystem rightBoosterParticle;
    [SerializeField] AudioSource mainEngineSound;
    [SerializeField] AudioSource leftEngineSound;
    [SerializeField] AudioSource rightEngineSound;

    //Cache
    Rigidbody rb;


    //State
    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("THIS IS THE PROTOTYPE BRANCH");
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
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
            rightEngineSound.Stop();
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
            leftEngineSound.Stop();
        }
    }
    private void RotateRight()
    {
        if (!rightBoosterParticle.isPlaying)
        {
            rightEngineSound.Play();
            rightBoosterParticle.Play();
        }
        ApplyRotation(-rotatePower);
    }
    private void RotateLeft()
    {
        if (!leftBoosterParticle.isPlaying)
        {
            leftEngineSound.Play();
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
        if (!mainEngineSound.isPlaying)
        {
            mainEngineSound.Play();
        }
        if (!mainBoosterParticle.isPlaying)
        {
            mainBoosterParticle.Play();
        }
    }
    private void StopThrusting()
    {
        mainEngineSound.Stop();
        mainBoosterParticle.Stop();
    }
}
