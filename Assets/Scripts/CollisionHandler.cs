using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float crashDelay = 1f;
    [SerializeField] float nextLevelDelay = 1f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    int currentSceneIndex;
    Movement movement;
    AudioSource audioSource;
    bool isTransitioning;
    
     
    void Awake()
    {
        movement = GetComponent<Movement>();
        audioSource = FindObjectOfType<AudioSource>();
    }
    
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Hello");
                break;

            case "Finish":
                Debug.Log("You have finished!");
                StartSucessSequence();
                break;

            default:
                Debug.Log("You crashed!");
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        movement.enabled = false;
        crashParticles.Play();
        Invoke("ReloadLevel", crashDelay);
    }

    void StartSucessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        successParticles.Play();
        Invoke("LoadNextLevel", nextLevelDelay);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {   
        currentSceneIndex++;
        if(currentSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            currentSceneIndex = 0;
        }
        SceneManager.LoadScene(currentSceneIndex);
        
    }

    
}
