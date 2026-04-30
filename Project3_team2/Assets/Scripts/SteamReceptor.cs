using UnityEngine;
using System.Collections;

public class SteamReceptor : MonoBehaviour
{
    private float collisionTimer = 0f;
    private bool isColliding = false;
    public float requiredTime = 3f;
    public bool steamable = true;
    public bool isHubReceptor = false;

    public float timeLimit;
    public PlayerMovement playerMovement;

    private AudioSource[] audioSources;

    public Light light1;
    public Light light2;

    private bool hasActivated = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SteamGun"))
        {
            isColliding = true;
            collisionTimer = 0f;

            if (steamable)
                audioSources[0].Play();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (isColliding && steamable)
        {
            Debug.Log("colliding");
            collisionTimer += Time.deltaTime;

            if (collisionTimer >= requiredTime)
            {
                Debug.Log("collided for " + requiredTime + " secs and no longer steamable");
                steamable = false;

                audioSources[1].Play();
                light1.enabled = true;
                light2.enabled = true;

                // regular levels receptors
                if (!isHubReceptor)
                {
                    playerMovement.activeSteamReceptors++;

                    if (playerMovement.onSteamTimer == false)
                    {
                        playerMovement.onSteamTimer = true;
                        Debug.Log("Timer started for " + timeLimit + " seconds omg run fr");
                        playerMovement.steamTimerLength = timeLimit;
                    }
                }
                else
                {
                    // hub shit
                    if (!hasActivated)
                    {
                        hasActivated = true;

                        if (GameProgress.Instance != null)
                        {
                            GameProgress.Instance.hubEnginesCount++;

                            Debug.Log("Hub engines count: " + GameProgress.Instance.hubEnginesCount);

                            if (GameProgress.Instance.hubEnginesCount >= 2)
                            {
                                GameProgress.Instance.hubEnginesActivated = true;

                                playerMovement.activeSteamReceptors = 2;

                                Debug.Log("HUB ENGINES ALL ACTIVATED YAAAAAAAAAAAY");
                            }
                        }
                    }
                }

                isColliding = false;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("collision exit");
        isColliding = false;
        collisionTimer = 0f;
    }

    void Start()
    {
        audioSources = GetComponents<AudioSource>();

        // keep hub receptors on if activated once already
        if (isHubReceptor && GameProgress.Instance != null && GameProgress.Instance.hubEnginesActivated)
        {
            steamable = false;
            light1.enabled = true;
            light2.enabled = true;

            if (playerMovement != null)
            {
                playerMovement.activeSteamReceptors = 2;
            }

            hasActivated = true;
        }
        else
        {
            light1.enabled = false;
            light2.enabled = false;
        }
    }

    void Update()
    {
        // only reset nonhub receptors
        if (!isHubReceptor)
        {
            if (playerMovement.onSteamTimer == false)
                steamable = true;
        }

        if (steamable)
        {
            light1.enabled = false;
            light2.enabled = false;
        }
        else
        {
            light1.enabled = true;
            light2.enabled = true;
        }
    }
}