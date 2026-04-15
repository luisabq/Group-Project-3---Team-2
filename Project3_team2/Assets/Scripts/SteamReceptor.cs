using UnityEngine;
using System.Collections;


public class SteamReceptor : MonoBehaviour
{



    private float collisionTimer = 0f;
    private bool isColliding = false;
    public float requiredTime = 3f;
    public bool steamable = true;
    //MeshRenderer mr; 


    public float timeLimit; 
    public PlayerMovement playerMovement;


    private AudioSource[] audioSources;

    public Light light1;
    public Light light2;


    // private Coroutine steamCoroutine;




    // if colliding with capsule, start count, at 3 seconds queue destroy

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SteamGun"))
        {
            isColliding = true;
            collisionTimer = 0f;

            if(steamable)
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
                
                //steamCoroutine = StartCoroutine(SteamTimerRoutine());
                playerMovement.activeSteamReceptors++;
                Debug.Log("steam count added, now " +  playerMovement.activeSteamReceptors);
                audioSources[1].Play();
                light1.enabled = true;
                light2.enabled = true;
                if (playerMovement.onSteamTimer == false)
                {
                    playerMovement.onSteamTimer = true;
                    Debug.Log("Timer started for " + timeLimit + " seconds omg run fr");
                    playerMovement.steamTimerLength = timeLimit;
                }


                //open portal or whatever steam is powering
                
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
        
        //mr = GetComponent<MeshRenderer>();
        GameObject.Find("Player").GetComponent<PlayerMovement>();
        audioSources = GetComponents<AudioSource>();
        light1.enabled = false;
        light2.enabled = false;
    }

    void Update()
    {
        if (playerMovement.onSteamTimer == false)
            steamable = true;
        if (steamable)
        {
            light1.enabled = false;
            light2.enabled = false;

            //mr.enabled = true;
        }
        else 
        {
           // mr.enabled = false;
            light1.enabled = true;
            light2.enabled = true;
        }

    }


    //old code for individual resetting of steam receptors

    //IEnumerator SteamTimerRoutine()
   // {
    //    yield return new WaitForSeconds(timeLimit);

      //  steamable = true;
       // Debug.Log("A receptor is now steamable");
        
   // }


}
