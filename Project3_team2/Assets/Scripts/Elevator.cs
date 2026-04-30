using UnityEngine;

public class Elevator : MonoBehaviour
{

    public SteamReceptor steamReceptor;
    private float speed = 1f;



    void Start()
    {
        
    }

    void Update()
    {


        if (steamReceptor.steamable == false)
        {
            Vector3 currentPos = transform.localPosition;
            Vector3 targetPos = new Vector3(currentPos.x, (currentPos.y - 6.96f), currentPos.z);
            transform.localPosition = Vector3.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);


        }

    }
}
