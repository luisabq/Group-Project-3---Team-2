using UnityEngine;

public class DisappearOnTouch : MonoBehaviour
{
        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);

        }
    }
}