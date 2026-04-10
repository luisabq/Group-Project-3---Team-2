using UnityEngine;

public class GrappleSystem : MonoBehaviour
{
    public float maxDistance = 20f;
    public LayerMask grappleLayer;
    public float grappleSpeed = 20f;

    private GrapplePoint currentTarget;
    private GrapplePoint previousTarget;

    private bool isGrappling = false;
    private Vector3 grapplePosition;

    void Update()
    {
        DetectGrapplePoint();

        if (Input.GetMouseButtonDown(1) && currentTarget != null)
        {
            StartGrapple();
        }

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
    }

    void StartGrapple()
    {
        isGrappling = true;
        grapplePosition = currentTarget.transform.position;
    }

    void MoveToGrapplePoint()
    {
        Vector3 direction = (grapplePosition - transform.position).normalized;
        transform.position += direction * grappleSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, grapplePosition) < 1f)
        {
            transform.position = grapplePosition;
            isGrappling = false;
        }
    }
}