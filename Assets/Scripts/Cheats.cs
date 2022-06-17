using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{
    [SerializeField] CapsuleCollider myCapsuleCollider;
    [SerializeField] BoxCollider myLeftBoxCollider;
    [SerializeField] BoxCollider myRightBoxCollider;

    int currentSceneIndex;
    bool isCollisionDisabled;
    

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        } 
        if(Input.GetKey(KeyCode.C))
        {
            DisableCollision();
        }
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

    void DisableCollision()
    {
        if(!isCollisionDisabled)
        {
            myCapsuleCollider.enabled = false;
            myLeftBoxCollider.enabled = false;
            myRightBoxCollider.enabled = false;
        } 
        else
        {
            myCapsuleCollider.enabled = true;
            myLeftBoxCollider.enabled = true;
            myRightBoxCollider.enabled = true;
        }

        isCollisionDisabled = !isCollisionDisabled;
        
    }
    
}
