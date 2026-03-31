using UnityEngine;
using Unity.Cinemachine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public CameraStyle currentStyle;

    public CinemachineCamera thirdPersonCam;
    public CinemachineCamera aimingCam;

    public Transform aimingLookAt;

    public enum CameraStyle
    {
        Basic,
        Aiming
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SwitchCameraStyle(CameraStyle.Basic);
    }

    private void Update()
    {
        // switch camera modes
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchCameraStyle(CameraStyle.Basic);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchCameraStyle(CameraStyle.Aiming);

        // camera orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        if (currentStyle == CameraStyle.Basic)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            Vector3 flatDir = inputDir;
            flatDir.y = 0f;

            if (inputDir.magnitude > 0.1f)
            {
                playerObj.forward = Vector3.Slerp(
                    playerObj.forward,
                    flatDir.normalized,
                    Time.deltaTime * rotationSpeed
                );
            }
        }
        else if (currentStyle == CameraStyle.Aiming)
        {
            Vector3 dirToAimingLookAt = aimingLookAt.position - new Vector3(transform.position.x, aimingLookAt.position.y, transform.position.z);

            orientation.forward = dirToAimingLookAt.normalized;
            playerObj.forward = dirToAimingLookAt.normalized;
        }
    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        if (newStyle == CameraStyle.Basic)
        {
            thirdPersonCam.Priority = 10;
            aimingCam.Priority = 0;
        }
        else
        {
            thirdPersonCam.Priority = 0;
            aimingCam.Priority = 10;
        }

        currentStyle = newStyle;
    }
}