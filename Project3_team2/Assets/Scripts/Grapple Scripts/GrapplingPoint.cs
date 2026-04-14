using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    [Header("References")]
    public GameObject visual;
    public GameObject outline;
    public Transform snapPoint;

    private bool isHighlighted = false;

    void Start()
    {
        if (outline != null)
            outline.SetActive(false);
    }

    public void Highlight()
    {
        isHighlighted = true;

        if (outline != null)
            outline.SetActive(true);
    }

    public void Unhighlight()
    {
        isHighlighted = false;

        if (outline != null)
            outline.SetActive(false);
    }
}