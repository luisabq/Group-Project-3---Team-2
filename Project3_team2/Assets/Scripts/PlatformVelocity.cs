using UnityEngine;

public class PlatformVelocity : MonoBehaviour
{
    public Vector3 Delta { get; private set; }

    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        Delta = transform.position - lastPosition;
        lastPosition = transform.position;
    }
}