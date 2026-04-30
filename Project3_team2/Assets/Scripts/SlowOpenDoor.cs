using System.Collections;
using UnityEditorInternal;
using UnityEngine;

public class SlowOpenDoor : MonoBehaviour
{


    public SteamReceptor steamReceptor;

    public float speed = 0.1f;

    public bool doorOpen = false;
    public float targetY = 0f;
    private float startY;

    void Start()
    {
        startY = transform.localPosition.y;
        targetY = transform.position.y + 6.96f;

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
            Vector3 targetPos = new Vector3(currentPos.x, targetY, currentPos.z);
            transform.localPosition = Vector3.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);
        }



    }
}
