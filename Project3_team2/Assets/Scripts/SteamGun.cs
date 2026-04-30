using UnityEngine;

public class SteamGun : MonoBehaviour
{
    public Transform playerTransform;
    public Transform cameraTransform;

    public GameObject steamCollider;
    public GameObject particles;

    private bool isFiring = false;

    public AudioSource steamSound;

    public Vector3 offset = new Vector3(0f, 0f, 0f);

    private void Start()
    {
        steamCollider.SetActive(false);
        particles.SetActive(false);
    }

    private void Update()
    {
        Vector3 worldOffset = cameraTransform.TransformDirection(offset);

        transform.position = cameraTransform.position + worldOffset;

        transform.rotation = cameraTransform.rotation;
    }

    public void Fire()
    {
        if (isFiring) return;

        isFiring = true;
        steamCollider.SetActive(true);
        particles.SetActive(true);
        steamSound.Play();
    }

    public void StopFire()
    {
        isFiring = false;
        steamCollider.SetActive(false);
        particles.SetActive(false);
        steamSound.Stop();
    }
}