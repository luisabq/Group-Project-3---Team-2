using UnityEngine;
using System.Collections;

public class DisappearOnTouch : MonoBehaviour
{
    public GameObject pickupText;
    public float textDuration = 2f;

    public AudioSource pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PickupSequence());
        }
    }

    IEnumerator PickupSequence()
    {
        if (pickupSound != null)
            pickupSound.Play();

        if (pickupText != null)
            pickupText.SetActive(true);

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        float waitTime = pickupSound != null ? pickupSound.clip.length : textDuration;

        yield return new WaitForSecondsRealtime(waitTime);

        if (pickupText != null)
            pickupText.SetActive(false);

        Destroy(gameObject);
    }
}