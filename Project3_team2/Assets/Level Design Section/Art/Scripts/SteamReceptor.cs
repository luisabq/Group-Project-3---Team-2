using UnityEngine;

public class SteamReceptor : MonoBehaviour
{

    private float collisionTimer = 0f;
    private bool isColliding = false;
    public float requiredTime = 3f;



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
            //Debug.Log("colliding");
            collisionTimer += Time.deltaTime;
            if (collisionTimer >= requiredTime)
            {
                Debug.Log("collided for 3 sec");
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
 
    }

    void Update()
    {

    }
}
