using UnityEngine;

public class Reset : MonoBehaviour
{

    public Rigidbody rb;
    public float dropDistance = 1000f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Drop();
        }
    }

    public void Drop()
    {
        rb.position += Vector3.down * dropDistance;
        rb.linearVelocity = Vector3.zero;
    }

   
}
