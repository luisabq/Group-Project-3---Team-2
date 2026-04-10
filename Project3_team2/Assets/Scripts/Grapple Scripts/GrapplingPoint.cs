using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    private Renderer rend;
    private Material mat;

    public Color normalColor = Color.white;
    public Color highlightColor = Color.cyan;

    private bool isHighlighted = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;

        mat.color = normalColor;
        mat.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        if (isHighlighted)
        {

            float pulse = Mathf.PingPong(Time.time * 2f, 1f) + 1f;
            transform.localScale = Vector3.one * pulse;

            mat.SetColor("_EmissionColor", highlightColor * 2f);
        }
        else
        {
            transform.localScale = Vector3.one;
            mat.SetColor("_EmissionColor", Color.black);
        }
    }

    public void Highlight()
    {
        isHighlighted = true;
    }

    public void Unhighlight()
    {
        isHighlighted = false;
    }
}