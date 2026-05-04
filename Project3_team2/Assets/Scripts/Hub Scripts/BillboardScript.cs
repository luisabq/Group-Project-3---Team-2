using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {
            transform.forward = cam.transform.forward;
        }
    }
}
