using UnityEngine;

public class RecallAbility : MonoBehaviour
{
    public Rigidbody rb;
    public Transform playerRoot;   //  object that moves
    public Transform playerObj;    // the visible model that rotates
    public Transform cameraTransform; // main camera transform

    private Vector3 recordPosition;
    private Vector3 recordDirection;

    public void SetRecallPoint()
    {
        recordPosition = playerRoot.position;

        // store ONLY horizontal direction
        recordDirection = playerObj.forward;
        recordDirection.y = 0f;
        recordDirection.Normalize();

        Debug.Log("Recall point set");
    }

    public void Recall()
    {
        // keeps current speed
        float currentSpeed = rb.linearVelocity.magnitude;

        // teleport
        playerRoot.position = recordPosition;

        // rotating player
        playerObj.forward = recordDirection;

        // applying momentum in stored direction
        rb.linearVelocity = recordDirection * currentSpeed;

        // camera matching direction (not working)
        Vector3 camDir = recordDirection;
        camDir.y = 0f;

        cameraTransform.rotation = Quaternion.LookRotation(camDir);

        Debug.Log("Recalled with speed: " + currentSpeed);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SetRecallPoint();
        if (Input.GetKeyDown(KeyCode.T)) Recall();
    }
}