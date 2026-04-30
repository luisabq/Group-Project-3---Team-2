using UnityEngine;

public class DoorOpening : MonoBehaviour
{


    public SteamReceptor steamReceptor;



    public bool doorOpen = false;

    public GameObject OpenDoor;
    public GameObject ClosedDoor;
    private bool hasTriggered = false;

    void Start()
    {

        ClosedDoor.SetActive(true);
        OpenDoor.SetActive(false);


    }

    void Update()
    {

        if (steamReceptor.steamable == false)
        {
            doorOpen = true;
        }

        if (doorOpen == true && hasTriggered == false)
        {

            ClosedDoor.SetActive(false);
            OpenDoor.SetActive(true);
            hasTriggered = true;
        }



    }
}
