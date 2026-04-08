using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SteamGun : MonoBehaviour
{


    public Transform playerTransform;

    public GameObject steamCollider;

    public GameObject particles;


    void Start()
    {

    }

    void Update()
    {
        // get player rotation
        float playerY = playerTransform.eulerAngles.y;

        // attach to player, in front 
        transform.position = playerTransform.position + (playerTransform.forward * 1);
        transform.eulerAngles = new Vector3(0, playerY, 0);

        // if pressing z, steam goes
        if (Input.GetKey(KeyCode.Z))
        {
            steamCollider.SetActive(true);
            particles.SetActive(true);
        }
        else
        {
            steamCollider.SetActive(false);
            particles.SetActive(false);
        }

    }
}
