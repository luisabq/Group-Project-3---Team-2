using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public CameraStyle currentStyle;

    public GameObject thirdPersonCam;
    public GameObject aimingCam;

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
    }

    private void Update()
    {
        // switch styles
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchCameraStyle(CameraStyle.Basic);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchCameraStyle(CameraStyle.Aiming);
        


        //rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        //rotate player obj
        if (currentStyle == CameraStyle.Basic)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
        else if ( currentStyle == CameraStyle.Aiming)
                {
                Vector3 dirToAimingLookAt = aimingLookAt.position - new Vector3(transform.position.x, aimingLookAt.position.y, transform.position.z);
                orientation.forward = dirToAimingLookAt.normalized;

                playerObj.forward = dirToAimingLookAt.normalized;
            }
        }

        private void SwitchCameraStyle(CameraStyle newStyle)
    {
        aimingCam.SetActive(false);
        thirdPersonCam.SetActive(false);
       

        if (newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
        if (newStyle == CameraStyle.Aiming) aimingCam.SetActive(true);

        currentStyle = newStyle;
    }

}

