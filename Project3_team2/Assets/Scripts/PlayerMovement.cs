using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private bool aimLockRotation;
    private bool isAiming;
    private Vector3 aimLockedDirection;
    public int activeSteamReceptors;
    public bool onSteamTimer = false;
    public float steamTimerLength;
    private Coroutine steamCoroutine;

    public Transform playerObj;

    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;

    public bool freeze;
    public bool unlimited;
    public bool restricted;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    private Rigidbody currentPlatformRb;
    private Vector3 platformVelocity;
    private Vector3 platformDelta;
    private PlatformVelocity currentPlatform;
    public Transform cameraTransform;

    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public AudioSource slowTimer;
    public AudioSource fastTimer;

    public MovementState state;

    public enum MovementState
    {
        freeze,
        unlimited,
        walking,
        sprinting,
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        onSteamTimer = false;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, playerHeight * 0.5f + 0.2f, whatIsGround))
        {
            grounded = true;
            Debug.Log("Grounded: true");

            PlatformVelocity pv = hit.collider.GetComponentInParent<PlatformVelocity>();

            if (pv != null)
            {
                currentPlatform = pv;
                platformDelta = pv.Delta;
                Debug.Log("Platform delta: " + platformDelta);
            }
            else
            {
                currentPlatform = null;
                platformDelta = Vector3.zero;
            }
        }
        else
        {
            grounded = false;
            currentPlatformRb = null;
            platformVelocity = Vector3.zero;
            Debug.Log("Grounded: false");

        }

        MyInput();
        SpeedControl();
        StateHandler();

        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;

        if (!onSteamTimer)
        {
            activeSteamReceptors = 0;
        }

        if (Input.GetMouseButtonDown(1))
        {
            isAiming = true;

            Vector3 dir = cameraTransform.forward;
            dir.y = 0f;
            aimLockedDirection = dir.normalized;

            playerObj.forward = aimLockedDirection;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isAiming = false;
        }

        if (isAiming)
        {
            playerObj.forward = aimLockedDirection;
        }
        else
        {
            if (moveDirection.sqrMagnitude > 0.01f)
            {
                playerObj.forward = Vector3.Slerp(
                    playerObj.forward,
                    moveDirection.normalized,
                    Time.deltaTime * 8f
                );
            }
        }

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void StateHandler()
    {
        if (freeze)
        {
            state = MovementState.freeze;
            rb.linearVelocity = Vector3.zero;
        }
        else if (unlimited)
        {
            state = MovementState.unlimited;
            moveSpeed = 999f;
            return;
        }

        if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        if (restricted) return;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        moveDirection = forward * verticalInput + right * horizontalInput;

        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.linearVelocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
        else if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        rb.useGravity = !OnSlope();

        if (grounded)
        {
            rb.MovePosition(rb.position + platformDelta);

        }
    }

    private void SpeedControl()
    {
        if (OnSlope() && !exitingSlope)
        {
            if (rb.linearVelocity.magnitude > moveSpeed)
                rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
        }
        else
        {
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}