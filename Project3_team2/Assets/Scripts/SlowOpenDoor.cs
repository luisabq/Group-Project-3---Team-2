using UnityEngine;

public class SlowOpenDoor : MonoBehaviour
{


    public SteamReceptor steamReceptor;

    public float speed = 0.1f;

    public bool doorOpen = false;
    private Quaternion targetRotation = Quaternion.Euler(0, -115, 0);

    void Start()
    {


    }

    void Update()
    {

        if (steamReceptor.steamable == false)
        {
            doorOpen = true;
        }

        if (doorOpen == true)
        {

            Vector3 currentPos = transform.localPosition;
            Vector3 targetPos = new Vector3(272.07f, currentPos.y, 72.13f);
            transform.localPosition = Vector3.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime );
        }



    }
}
