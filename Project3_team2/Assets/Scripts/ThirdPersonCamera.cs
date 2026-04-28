using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform combatLookAt;

    public CinemachineCamera thirdPersonCam;
    public CinemachineCamera combatCam;
    public CinemachineCamera topDownCam;

    public CameraStyle currentStyle;

    public enum CameraStyle
    {
        Basic,
        Combat,
        Topdown
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SwitchCameraStyle(CameraStyle.Basic);
    }

    private void Update()
    {
        HandleCameraSwitch();
        HandleRotation();
    }

    void HandleCameraSwitch()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SwitchCameraStyle(CameraStyle.Combat);
        }

        if (Input.GetMouseButtonUp(1))
        {
            SwitchCameraStyle(CameraStyle.Basic);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchCameraStyle(CameraStyle.Topdown);
        }
    }

    void HandleRotation()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        if (currentStyle == CameraStyle.Combat)
        {
            Vector3 dirToLook = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);

            orientation.forward = dirToLook.normalized;
            
        }
        else
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            
        }
    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        if (currentStyle == newStyle) return;

        thirdPersonCam.Priority = 0;
        combatCam.Priority = 0;
        topDownCam.Priority = 0;

        if (newStyle == CameraStyle.Basic)
            thirdPersonCam.Priority = 10;

        if (newStyle == CameraStyle.Combat)
            combatCam.Priority = 10;

        if (newStyle == CameraStyle.Topdown)
            topDownCam.Priority = 10;

        currentStyle = newStyle;
    }
}