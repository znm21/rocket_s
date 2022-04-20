using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 20f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem  mainEngineParticle,leftThrusterParticle,rightThrusterParticle;

    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
            
        }else{
            StopThrusting();
    }
}
    void StartThrusting()
{
    rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    if (!audioSource.isPlaying)
    {
        audioSource.PlayOneShot(mainEngine);
    }
    if (!mainEngineParticle.isPlaying)
    {
        mainEngineParticle.Play();

    }
}
    void StopThrusting()
    {
            audioSource.Stop();
            mainEngineParticle.Stop();

    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();

        } else
        {
            StopRotating();
        }
    
        void RotateLeft()
        {

            ApplayRotation(rotationThrust);
            if (!leftThrusterParticle.isPlaying)
            {
                leftThrusterParticle.Play();
            }
        }
        void RotateRight()
        {
            ApplayRotation(-rotationThrust);
            if (!rightThrusterParticle.isPlaying)
            {
                rightThrusterParticle.Play();

            }
        }
        void StopRotating()
        {
            leftThrusterParticle.Stop();
            rightThrusterParticle.Stop();

        }
        void ApplayRotation(float rotationThisFrame)
        {
            rb.freezeRotation = true; //Freezing rotatiton so we can manually move our Rocket
            transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
            rb.freezeRotation = false; //unFreezing rotatiton 
        }
    }
}
