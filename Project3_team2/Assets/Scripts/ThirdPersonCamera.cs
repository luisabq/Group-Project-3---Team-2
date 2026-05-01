using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform combatLookAt;

    public GameObject thirdPersonCam;
    public GameObject combatCam;
    public GameObject topDownCam;

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
        bool aiming = Input.GetMouseButton(1);

        if (aiming && currentStyle != CameraStyle.Combat)
        {
            SwitchCameraStyle(CameraStyle.Combat);
        }
        else if (!aiming && currentStyle == CameraStyle.Combat)
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
            playerObj.forward = dirToLook.normalized;
        }
        else
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
        }
    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        if (currentStyle == newStyle) return;

        combatCam.SetActive(false);
        thirdPersonCam.SetActive(false);
        topDownCam.SetActive(false);

        if (newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
        if (newStyle == CameraStyle.Combat) combatCam.SetActive(true);
        if (newStyle == CameraStyle.Topdown) topDownCam.SetActive(true);

        currentStyle = newStyle;
    }
}