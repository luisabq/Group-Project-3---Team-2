using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public Animator animator; 
    public Rigidbody rb;

    void Update()
    {
        float speed = rb.linearVelocity.magnitude;
        
        animator.SetFloat("Speed", speed);

        if (Input.GetMouseButton(1)) 
        {
            animator.SetBool("Aiming", true);
        }
        else
        {
            animator.SetBool("Aiming", false);
        }
    }
}