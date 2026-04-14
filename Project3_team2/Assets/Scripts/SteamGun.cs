using UnityEngine;

public class SteamGun : MonoBehaviour
{


    public Transform playerTransform;

    public GameObject steamCollider;

    public GameObject particles;

    public bool equipped = false; 


    void Start()
    {
        particles.transform.position = transform.position + (playerTransform.up * 0.5f);
        equipped = false; 
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            equipped = true;
            Debug.Log("equipped steam");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            equipped = false;
            Debug.Log("unequipped steam");
        }



        // get player rotation
        float playerY = playerTransform.eulerAngles.y;

        // attach to player, in front 
        transform.position = playerTransform.position + (playerTransform.forward * 1);
        transform.eulerAngles = new Vector3(0, playerY, 0);


        if (equipped)
        {

            // if pressing z, steam goes
            if (Input.GetKeyDown(KeyCode.Z))
            {
                steamCollider.SetActive(true);
                particles.SetActive(true);
            }
           

        }
        if (!equipped || Input.GetKeyUp(KeyCode.Z)) 
        {
            steamCollider.SetActive(false);
            particles.SetActive(false);
        }

    }
}
