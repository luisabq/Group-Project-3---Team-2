using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    [Header("References")]
    public GameObject visual;
    public GameObject outline;
    public Transform snapPoint;
    private AudioSource audioSource;

    private bool isHighlighted = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (outline != null)
            outline.SetActive(false);
    }

    public void Highlight()
    {
        audioSource.Play();
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