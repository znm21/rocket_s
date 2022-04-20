using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadLevelDelay = 1f;
    [SerializeField] AudioClip sucesse, crash;
    [SerializeField] ParticleSystem sucesseParticle, crashParticle;
    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisabeld = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        RespondToDebugKeys();
    }
    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabeld = !collisionDisabeld;//toggle collision
        }

        }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabeld) {
            return; }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friend");
                break;
            case "Finish":
                StartSucessesSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }
       void StartSucessesSequence()
        {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(sucesse);
        sucesseParticle.Play();
        GetComponent<Movement>().enabled = false;
            Invoke("LoadNextLevel", loadLevelDelay);
        }
        void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
            Invoke("ReloadLevel", loadLevelDelay);
            
        }
        void LoadNextLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextIndexScene = currentSceneIndex + 1;
            if(nextIndexScene == SceneManager.sceneCountInBuildSettings)
            {
                nextIndexScene = 0;
            }
            SceneManager.LoadScene(nextIndexScene);
        }

        void ReloadLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneIndex);
        }
    }
    

