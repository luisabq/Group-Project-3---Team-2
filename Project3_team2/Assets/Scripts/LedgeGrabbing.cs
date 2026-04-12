using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrabbing : MonoBehaviour
{
    [Header("References")]
    public PlayerMovement pm;
    public Transform orientation;
    public Transform cam;
    public Rigidbody rb;

    [Header("Ledge Grabbing")]
    public float moveToLedgeSpeed;
    public float maxLedgeGrabDistance;
    public float minTimeOnLedge;

    private float timeOnLedge;
    public bool holding;

    [Header("Ledge Jumping")]
    public KeyCode jumpKey = KeyCode.Space;
    public float ledgeJumpForwardForce;
    public float ledgeJumpUpwardForce;

    [Header("Ledge Detection")]
    public float ledgeDetectionLength;
    public float ledgeSphereCastRadius;
    public LayerMask whatIsLedge;

    private RaycastHit ledgeHit;
    private Vector3 ledgePoint;
    private Vector3 ledgeNormal;
    private bool hasLedge;

    [Header("Exiting")]
    public bool exitingLedge;
    public float exitLedgeTime;
    private float exitLedgeTimer;

    void Update()
    {
        LedgeDetection();
        SubStateMachine();
    }

    private void LedgeDetection()
    {
        if (holding) return;

        Vector3 camFlat = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;

        if (Physics.SphereCast(transform.position, ledgeSphereCastRadius, camFlat, out ledgeHit, ledgeDetectionLength, whatIsLedge))
        {
            Vector3 checkPoint = ledgeHit.point + Vector3.up * 0.5f;

            if (Physics.Raycast(checkPoint, Vector3.down, 1.5f, whatIsLedge))
            {
                float dist = Vector3.Distance(transform.position, ledgeHit.point);

                if (dist < maxLedgeGrabDistance)
                {
                    ledgePoint = ledgeHit.point;
                    ledgeNormal = ledgeHit.normal;
                    hasLedge = true;

                    EnterLedgeHold();
                }
            }
        }
    }

    private void SubStateMachine()
    {
        if (!holding) return;

        timeOnLedge += Time.deltaTime;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if ((h != 0 || v != 0) && timeOnLedge > minTimeOnLedge)
            ExitLedgeHold();

        if (Input.GetKeyDown(jumpKey))
            LedgeJump();
    }

    private void EnterLedgeHold()
    {
        holding = true;
        exitingLedge = false;
        timeOnLedge = 0f;

        pm.restricted = true;
        pm.freeze = true;

        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = ledgePoint + ledgeNormal * 0.3f;
    }

    private void FreezeOnLedge()
    {
        if (!holding) return;

        Vector3 target = ledgePoint + ledgeNormal * 0.3f;
        transform.position = Vector3.Lerp(transform.position, target, moveToLedgeSpeed * Time.deltaTime);
    }

    private void LedgeJump()
    {
        ExitLedgeHold();

        Vector3 forward = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
        Vector3 up = Vector3.up;
        Vector3 away = ledgeNormal;

        Vector3 jumpDir = (forward + away * 0.8f + up * 1.2f).normalized;

        rb.linearVelocity = Vector3.zero;
        rb.AddForce(jumpDir * ledgeJumpForwardForce, ForceMode.Impulse);
    }

    private void ExitLedgeHold()
    {
        holding = false;
        exitingLedge = true;
        exitLedgeTimer = exitLedgeTime;
        timeOnLedge = 0f;

        pm.restricted = false;
        pm.freeze = false;

        rb.useGravity = true;

        Invoke(nameof(ResetLedge), 0.5f);
    }

    private void ResetLedge()
    {
        hasLedge = false;
    }
}