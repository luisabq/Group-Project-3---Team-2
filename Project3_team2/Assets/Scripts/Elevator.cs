using System.Collections;
using UnityEditorInternal;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    public float speed = 1f;
    public float targetY = 0f;
    private float startY;
    public float platformTimeLimit;
    public SteamReceptor steamReceptor;

    private bool notStart = false;
    private bool timeGoing = false;
    private bool hasTriggered = false;
    private bool noRepeat = false;

    void Start()
    {

        startY = transform.localPosition.y;
        targetY = transform.position.y - 6.96f;
    }


    IEnumerator WaitForPlatformTimeLimit(float delay)
    {
        yield return new WaitForSeconds(delay);
        steamReceptor.steamable = true;
        noRepeat = false;
    }


    void Update()
    {
        // platforms going out, starts timer 
        if (steamReceptor.steamable == false)
        {
            Vector3 currentPos = transform.localPosition;
            Vector3 targetPos = new Vector3(currentPos.x, targetY, currentPos.z);
            transform.localPosition = Vector3.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);
            notStart = true;
            hasTriggered = true;
        }

        // platform going back in
        if (steamReceptor.steamable == true && notStart == true)
        {
            Vector3 targetPos = transform.localPosition;
            Vector3 currentPos = new Vector3(targetPos.x, startY, targetPos.z);
            transform.localPosition = Vector3.MoveTowards(targetPos, currentPos, speed * Time.deltaTime);
        }

        // starts coroutine timer
        if (timeGoing == true)
        {
            StartCoroutine(WaitForPlatformTimeLimit(platformTimeLimit));
            timeGoing = false;
        }

        // this is so the timer doesn't start multiple times 
        if (hasTriggered == true && noRepeat == false)
        {
            noRepeat = true;
            timeGoing = true;
        }


    }

}