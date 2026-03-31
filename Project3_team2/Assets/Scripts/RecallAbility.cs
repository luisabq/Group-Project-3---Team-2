using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class RecallAbility : MonoBehaviour
{
    public Rigidbody rb;
    public Transform playerRoot;   //  object that moves
    public Transform playerObj;    // the visible model that rotates
    public Transform cameraTransform; // main camera transform

    public Quaternion playerRot;

    private Vector3 recordPosition;
    private Vector3 recordDirection;

    private Vector3 currentRotation;

    public GameObject Portal;
    private GameObject spawnedObject;


    public bool hasTimer;
    public float teleportTime;

    public void SetRecallPoint()
    {
        if (hasTimer)
        {
            StartCoroutine(DoActionAfterDelay(teleportTime));
        }

        Destroy(spawnedObject);

        recordPosition = playerRoot.position;

        // store ONLY horizontal direction
        recordDirection = playerObj.forward;
        recordDirection.y = 0f;
        recordDirection.Normalize();

        Quaternion portalRotation = Quaternion.LookRotation(recordDirection);

        spawnedObject = Instantiate(Portal, recordPosition, portalRotation);
        spawnedObject.SetActive(true);

        Debug.Log("Recall point set");
    }

    public void Recall()
    {
        spawnedObject.SetActive(false);

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
        //Vector3 currentRotation = transform.eulerAngles;

        // Replace only the Y-axis with player's Y rotation
       // currentRotation.y = playerObj.eulerAngles.y;

        // Apply the new rotation
        //transform.rotation = Quaternion.Euler(currentRotation);

        if (Input.GetKeyDown(KeyCode.R)) SetRecallPoint();
        if (Input.GetKeyDown(KeyCode.T)) Recall();
    }




    private System.Collections.IEnumerator DoActionAfterDelay(float teleportTime)
    {
        // Validate delay to avoid negative values
        if (teleportTime < 0f)
        {
            Debug.LogWarning("Delay cannot be negative. Using 0 instead.");
            teleportTime = 0f;
        }

        // Wait for the specified time without freezing the game
        yield return new WaitForSeconds(teleportTime);

        // Perform your action here
        Debug.Log($"Action executed after {teleportTime} seconds!");

        // Example: Enable a GameObject
        Recall();
    }

}