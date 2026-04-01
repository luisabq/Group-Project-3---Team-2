using System.Collections;
using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;

public class RecallAbility : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public Transform playerRoot;
    public Transform playerObj;

    [Header("Portal")]
    public GameObject Portal;
    private GameObject spawnedPortal;

    [Header("Cinemachine")]
    public CinemachineCamera portalCamera;
    public int portalCamPriority = 20;
    public float portalCamDuration = 1f; // how long portal camera stays active
    private Dictionary<CinemachineCamera, int> originalPriorities = new Dictionary<CinemachineCamera, int>();
    private CinemachineCamera[] allCams;

    [Header("Teleport Settings")]
    public bool hasTimer;
    public float teleportTime;

    private Vector3 recordPosition;
    private Vector3 recordDirection;

    [Header("Portal Camera Offset")]
    public Vector3 behindOffset = new Vector3(0, 2f, -4f);

    public bool usingCam;

    private void Start()
    {
        allCams = FindObjectsOfType<CinemachineCamera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SetRecallPoint();
        if (Input.GetKeyDown(KeyCode.T)) Recall();
    }

    public void SetRecallPoint()
    {
        if (hasTimer)
            StartCoroutine(DoActionAfterDelay(teleportTime));

        Destroy(spawnedPortal);

        recordPosition = playerRoot.position;

        recordDirection = playerObj.forward;
        recordDirection.y = 0f;
        recordDirection.Normalize();

        Quaternion portalRotation = Quaternion.LookRotation(recordDirection);
        spawnedPortal = Instantiate(Portal, recordPosition, portalRotation);
        spawnedPortal.SetActive(true);

        // setup portal camera to follow portal
        if (portalCamera != null && usingCam)
        {
            portalCamera.Follow = spawnedPortal.transform;
            portalCamera.LookAt = spawnedPortal.transform;

            var transposerBase = portalCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
            var transposer = transposerBase as CinemachineTransposer;
            if (transposer != null)
                transposer.m_FollowOffset = behindOffset;
        }

        Debug.Log("Recall point set");
    }

    public void Recall()
    {
        if (spawnedPortal != null)
            spawnedPortal.SetActive(false);

        float currentSpeed = rb.linearVelocity.magnitude;

        // teleport player
        playerRoot.position = recordPosition;
        playerObj.forward = recordDirection;
        rb.linearVelocity = recordDirection * currentSpeed;

        // camera switching like CameraTriggerZone
        if (portalCamera != null && usingCam)
        {
            originalPriorities.Clear();

            foreach (var cam in allCams)
            {
                if (cam == null) continue;

               
                originalPriorities[cam] = cam.Priority;

               
                if (cam != portalCamera)
                    cam.Priority = 0;
            }

           
            portalCamera.Priority = portalCamPriority;

            // restore after delay
            StartCoroutine(RestoreCameraPriorities());
        }

        Debug.Log("Recalled with speed: " + currentSpeed);
    }

    private IEnumerator RestoreCameraPriorities()
    {
        yield return new WaitForSeconds(portalCamDuration);

        foreach (var pair in originalPriorities)
        {
            if (pair.Key != null)
                pair.Key.Priority = pair.Value;
        }
    }

    private IEnumerator DoActionAfterDelay(float delay)
    {
        if (delay < 0f) delay = 0f;
        yield return new WaitForSeconds(delay);
        Recall();
    }
}