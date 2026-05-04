using UnityEngine;

public class CanyonTrigger : MonoBehaviour
{
    [Header("References")]
    public AudioManager audioManager;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;

            if (audioManager != null)
            {
                audioManager.EnterCanyon();
            }
        }
    }
}