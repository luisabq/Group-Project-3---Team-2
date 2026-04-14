using UnityEngine;
using System.Collections;


public class SteamReceptor : MonoBehaviour
{

    private float collisionTimer = 0f;
    private bool isColliding = false;
    public float requiredTime = 3f;
    public bool steamable = true;
    MeshRenderer mr; 


    public float timeLimit; 
    public PlayerMovement playerMovement;

   // private Coroutine steamCoroutine;




    // if colliding with capsule, start count, at 3 seconds queue destroy

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SteamGun"))
        {
            isColliding = true;
            collisionTimer = 0f;
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
        mr = GetComponent<MeshRenderer>();
        GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (playerMovement.onSteamTimer == false)
            steamable = true;
        if (steamable)
            mr.enabled = true;
        else mr.enabled = false;

    }


    //old code for individual resetting of steam receptors

    //IEnumerator SteamTimerRoutine()
   // {
    //    yield return new WaitForSeconds(timeLimit);

      //  steamable = true;
       // Debug.Log("A receptor is now steamable");
        
   // }


}
