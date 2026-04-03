using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class LedgeGrabbing : MonoBehaviour
{

    public PlayerMovement pm;
    public Transform orientation;
    public Transform cam;
    public Rigidbody rb;

    public float moveToLedgeSpeed;
    public float maxLedgeGrabDistance;

    public float minTimeOnLedge;
    private float timeOnLedge;

    public bool holding;

    public float ledgeDetectionLength;
    public float ledgeSphereCastRadius;
    public LayerMask whatIsLedge;

    private Transform lastLedge;
    private Transform currLedge;

    private RaycastHit ledgeHit; 


    private void LedgeDetection()
    {
        //needs code changed for third person camera 
        bool ledgeDected = Physics.SphereCast(transform.position, ledgeSphereCastRadius, cam.forward,out ledgeHit, ledgeDetectionLength, whatIsLedge);
        if (!ledgeDected) return;

        float distanceToLedge = Vector3.Distance(transform.position, ledgeHit.transform.position);

        if (distanceToLedge < maxLedgeGrabDistance && !holding) EnterLedgeHold();
    }

    private void EnterLedgeHold()
    {
        holding = true;

        currLedge = ledgeHit.transform;
        lastLedge = ledgeHit.transform;

        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
    }

    private void FreezeRigidbodyOnLedge()
    {

    }


    private void ExitLedgeHold()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LedgeDetection();
    }
}
