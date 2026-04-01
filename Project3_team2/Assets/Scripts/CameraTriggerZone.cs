using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CameraTriggerZone : MonoBehaviour
{
    public CinemachineCamera overrideCamera;
    public int overridePriority = 20;

    private Dictionary<CinemachineCamera, int> originalPriorities =
        new Dictionary<CinemachineCamera, int>();

    private CinemachineCamera[] allCams;

    private void Start()
    {
        // Get all cameras 
        allCams = FindObjectsOfType<CinemachineCamera>();
    }

    private void OnTriggerEnter(Collider other)

    {
        Debug.Log("entered");
        if (!other.CompareTag("Player")) return;

        originalPriorities.Clear();

        foreach (var cam in allCams)
        {
            if (cam == null) continue;

            // saving original priority
            originalPriorities[cam] = cam.Priority;

            // unprioritizing everything else
            if (cam != overrideCamera)
                cam.Priority = 0;
        }

        // prioritizing specificed cam
        if (overrideCamera != null)
            overrideCamera.Priority = overridePriority;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Restore priorities
        foreach (var pair in originalPriorities)
        {
            if (pair.Key != null)
                pair.Key.Priority = pair.Value;
        }
    }
}