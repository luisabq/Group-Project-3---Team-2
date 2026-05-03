
using UnityEngine;
using System.Collections;

public class GrappleSystem : MonoBehaviour
{
    public float maxDistance = 20f;
    public LayerMask grappleLayer;
    public float grappleSpeed = 20f;

    public PlayerMovement playerMovement;

    private GrapplePoint currentTarget;
    private GrapplePoint previousTarget;

    public bool isGrappling = false;
    private Vector3 grapplePosition;

    public AudioSource audioSource;

    public LineRenderer rope;
    public Transform ropeStart;
    private Vector3 ropeVisualPoint;


    public void TryGrapple()
    {
        if (currentTarget != null)
        {
            audioSource.Play();
            StartGrapple();
        }
    }
    void Update()
    {
        if (isGrappling)
        {
            MoveToGrapplePoint();
            UpdateRope();
        }
        else
        {
            if (rope != null)
                rope.enabled = false;
        }

        DetectGrapplePoint();

        if (isGrappling)
        {

            MoveToGrapplePoint();
        }
    }

    void DetectGrapplePoint()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, grappleLayer))
        {
            GrapplePoint newTarget = hit.collider.GetComponent<GrapplePoint>();

            if (newTarget != currentTarget)
            {
                if (previousTarget != null)
                    previousTarget.Unhighlight();

                currentTarget = newTarget;
                previousTarget = newTarget;

                if (currentTarget != null)
                    currentTarget.Highlight();
            }
        }
        else
        {
            if (currentTarget != null)
            {
                currentTarget.Unhighlight();
                currentTarget = null;
                previousTarget = null;
            }
        }

        if (Physics.Raycast(ray, out hit, maxDistance, grappleLayer))
        {
            ropeVisualPoint = hit.point + hit.normal * 0.05f;

            GrapplePoint newTarget = hit.collider.GetComponent<GrapplePoint>();

        }
    }

    void StartGrapple()
    {

        isGrappling = true;

        // pls work
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if (currentTarget.snapPoint != null)
            grapplePosition = currentTarget.snapPoint.position;
        else
            grapplePosition = currentTarget.transform.position;

        grapplePosition += Vector3.up * 0.5f;





        if (rope != null)
            rope.enabled = true;
    }
    void FinishGrapple()
    {
        isGrappling = false;

        transform.position = grapplePosition;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (rope != null)
            rope.enabled = false;

        if (playerMovement != null)
            playerMovement.enabled = true;


    }
    void MoveToGrapplePoint()
    {
        float step = grappleSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, grapplePosition, step);

        if (Vector3.Distance(transform.position, grapplePosition) < 0.1f)
        {
            FinishGrapple();
        }
    }


    void UpdateRope()
    {
        if (rope == null) return;

        rope.enabled = true;

        rope.SetPosition(0, ropeStart.position);
        rope.SetPosition(1, ropeVisualPoint);
    }



}
