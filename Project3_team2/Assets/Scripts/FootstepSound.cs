using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepClips;

    [Header("Pitch Randomization")]
    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;

    [Header("Ground Check")]
    public Rigidbody rb;
    public LayerMask groundMask;
    public float groundCheckDistance = 0.3f;

    private bool isGrounded;

    void Update()
    {
        CheckGrounded();
    }

    void CheckGrounded()
    {
        isGrounded = Physics.Raycast(
            transform.position + Vector3.up * 0.1f,
            Vector3.down,
            groundCheckDistance,
            groundMask
        );
    }

    public void PlayFootstep()
    {
        if (!isGrounded) return;
        if (footstepClips.Length == 0 || audioSource == null) return;

        // optional: stop tiny jitter footsteps when barely moving
        Vector3 horizontalVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (horizontalVel.magnitude < 0.2f) return;

        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];

        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(clip);
    }
}