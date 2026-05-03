using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rb;
    public FootstepSound footstepSound;
    public GrappleSystem grappleSystem;
    

    void Update()
    {
        float speed = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z).magnitude;
        float yVel = rb.linearVelocity.y;
        
        float lt = Input.GetAxis("LT");
        



        animator.SetFloat("Speed", speed);
        animator.SetFloat("YVelocity", yVel);

        animator.SetBool("Grounded", footstepSound.isGrounded);



        animator.SetBool("Aiming", Input.GetMouseButton(1) || lt > 0.1f);

        if (Input.GetKeyDown(KeyCode.Space) && footstepSound.isGrounded)
        {
            animator.SetTrigger("Jump");
        }

       
           animator.SetBool("Grapple", grappleSystem.isGrappling);
       
       

        void Start()
        {
            grappleSystem = GetComponent<GrappleSystem>();
           
        }

    }
}