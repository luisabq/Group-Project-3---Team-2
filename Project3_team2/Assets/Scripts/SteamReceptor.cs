using UnityEngine;

public class SteamReceptor : MonoBehaviour
{

    private float collisionTimer = 0f;
    private bool isColliding = false;
    public float requiredTime = 3f;


    public float timeLimit; 
    public PlayerMovement playerMovement;




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
        if (isColliding)
        {
            Debug.Log("colliding");
            collisionTimer += Time.deltaTime;
            playerMovement.onSteamTimer = true;

            //start coroutine on player counting for timeLimit seconds. once its done, set on timer to false and...but wait time limit seconds is 
            //set on here, cause it needs to be...but i cant plug in every single steam receptor into the player...

            if (collisionTimer >= requiredTime)
            {
                Debug.Log("collided for 3 sec");

                playerMovement.activeSteamReceptors++; 


                //open portal or whatever steam is powering
                Destroy(gameObject);
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
        GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {

    }
}
