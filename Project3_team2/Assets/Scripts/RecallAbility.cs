using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class RecallAbility : MonoBehaviour
{
    public Rigidbody rb;
    public Transform playerRoot;   //  object that moves
    public Transform playerObj;    // the visible model that rotates
    public Transform cameraTransform; // main camera transform

    private Vector3 recordPosition;
    private Vector3 recordDirection;

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

        spawnedObject = Instantiate(Portal, recordPosition, Quaternion.identity);
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