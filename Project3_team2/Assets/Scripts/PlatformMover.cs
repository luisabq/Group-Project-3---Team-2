using System.Collections;
using UnityEditorInternal;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{

    public float speed = 0.05f;
    public float targetZ = 0f;
    private float startZ;
    public float platformTimeLimit;
    public SteamReceptor steamReceptor;

    private bool notStart = false;
    public AudioSource platformSound;
    // private bool timeGoing = false;
    // private bool hasTriggered = false;
    //private bool noRepeat = false;

    void Start()
    {

        startZ = transform.localPosition.z;

    }


    //IEnumerator WaitForPlatformTimeLimit(float delay)
    //{
    //   yield return new WaitForSeconds(delay);
    //   steamReceptor.steamable = true;
    //    noRepeat = false;
    //  }


    void Update()
    {
        // platforms going out, starts timer 
        if (steamReceptor.steamable == false)
        {
            platformSound.Play();
            Vector3 currentPos = transform.localPosition;
            Vector3 targetPos = new Vector3(currentPos.x, currentPos.y, targetZ);
            transform.localPosition = Vector3.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);
            // notStart = true;
            // hasTriggered = true;
        }
        else
        {

        }

        // platform going back in
        // if (steamReceptor.steamable == true && notStart == true)
        //  {
        //  Vector3 targetPos = transform.localPosition;
        //   Vector3 currentPos = new Vector3(targetPos.x, targetPos.y, startZ);
        //   transform.localPosition = Vector3.MoveTowards(targetPos, currentPos, speed * Time.deltaTime);
        // }

        // starts coroutine timer
        //if (timeGoing == true)
        //{
        //     StartCoroutine(WaitForPlatformTimeLimit(platformTimeLimit));
        //     timeGoing = false;
        //  }

        // this is so the timer doesn't start multiple times 
        // if (hasTriggered == true && noRepeat == false)
        // {
        //    noRepeat = true;
        //      timeGoing = true;
        //  }


    }

}