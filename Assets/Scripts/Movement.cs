using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 10f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip engineThrust;
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;


    Rigidbody rocketRB;
    AudioSource audioSource;
    

    void Awake()
    {
        rocketRB = FindObjectOfType<Rigidbody>();
        audioSource = FindObjectOfType<AudioSource>();
    }


    void Update()
    {
       ProcessThrust(); 
       ProcessRotation();
    }

   
    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            RotateRight();
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }
    }


    void ApplyRotation(float rotationThisFrame)
    {   
        rocketRB.freezeRotation = true; // freezing rotation so we can manyally rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketRB.freezeRotation = false; 
    }

    void StartThrusting()
    {
        if (!mainThrustParticles.isPlaying)
        {
            mainThrustParticles.Play();
        }

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(engineThrust);
        }
        rocketRB.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }

    void StopThrusting()
    {
        mainThrustParticles.Stop();
        // audioSource.Stop();
    }

    void StartSideThrusting(String type)
    {
        if (type == "right")
        {
            
        }
        else
        {
            if(!leftThrustParticles.isPlaying)
            {
                leftThrustParticles.Play();
            }
        }
    }

    void RotateRight()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }

    void StopRotating()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }
}
