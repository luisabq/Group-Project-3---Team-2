using UnityEngine;

public class RecallAbility : MonoBehaviour
{
    public Rigidbody rb;
    public Transform playerRoot;   // the object that moves (your player)
    public Transform playerObj;    // the visible model that rotates
    public Transform cameraTransform; // MAIN CAMERA transform

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
        // keep CURRENT speed (not old speed)
        float currentSpeed = rb.linearVelocity.magnitude;

        // TELEPORT
        playerRoot.position = recordPosition;

        // ROTATE PLAYER
        playerObj.forward = recordDirection;

        // APPLY MOMENTUM in stored direction
        rb.linearVelocity = recordDirection * currentSpeed;

        // FORCE CAMERA TO MATCH DIRECTION
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