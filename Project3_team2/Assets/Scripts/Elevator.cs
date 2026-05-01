using System.Collections;
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
    public AudioSource elevatorSound;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startY = transform.localPosition.y;
        targetY = transform.position.y - 7.9f;
    }


    IEnumerator WaitForPlatformTimeLimit(float delay)
    {
        yield return new WaitForSeconds(delay);
        steamReceptor.steamable = true;
        noRepeat = false;
    }


    void Update()
    {

        bool moving = false;

        if (steamReceptor.steamable == false)
        {
            Vector3 currentPos = transform.localPosition;
            Vector3 targetPos = new Vector3(currentPos.x, targetY, currentPos.z);

            if (currentPos != targetPos)
            {
                transform.localPosition = Vector3.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);
                moving = true;
            }

            notStart = true;
            hasTriggered = true;
        }

        if (steamReceptor.steamable == true && notStart == true)
        {
            Vector3 currentPos = transform.localPosition;
            Vector3 targetPos = new Vector3(currentPos.x, startY, currentPos.z);

            if (currentPos != targetPos)
            {
                transform.localPosition = Vector3.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);
                moving = true;
            }
        }

        if (moving)
        {
            if (!elevatorSound.isPlaying)
                elevatorSound.Play();
        }
        else
        {
            if (elevatorSound.isPlaying)
                elevatorSound.Stop();
        }

        if (timeGoing == true)
        {
            StartCoroutine(WaitForPlatformTimeLimit(platformTimeLimit));
            timeGoing = false;
        }

        if (hasTriggered == true && noRepeat == false)
        {
            noRepeat = true;
            timeGoing = true;
        }
    }

}