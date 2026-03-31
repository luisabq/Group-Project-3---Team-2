using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class LedgeGrabbing : MonoBehaviour
{

    public PlayerMovement pm;
    public Transform orientation;
    public Transform cam;
    public Rigidbody rb;

    public float ledgeDetectionLength;
    public float ledgeSphereCastRadius;
    public LayerMask whatIsLedge;

    private Transform lastLedge;
    private Transform currLedge;

    private RaycastHit ledgeHit; 


    private void LedgeDetection()
    {
        //needs code changed for third person camera 
        //bool ledgeDected = Physics.SphereCast(transform.position, ledgeSphereCastRadius, out ledgeHit, ledgeDetectionLength, whatIsLedge);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
