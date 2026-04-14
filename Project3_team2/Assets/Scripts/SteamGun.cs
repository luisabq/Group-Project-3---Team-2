using UnityEngine;

public class SteamGun : MonoBehaviour
{
    public Transform playerTransform;

    public GameObject steamCollider;
    public GameObject particles;

    private bool isFiring = false;

    private void Start()
    {
        steamCollider.SetActive(false);
        particles.SetActive(false);
    }

    private void Update()
    {

        transform.position = playerTransform.position + (playerTransform.forward * 1);

        transform.forward = Camera.main.transform.forward;

    }

    public void Fire()
    {
        if (isFiring) return;

        isFiring = true;
        steamCollider.SetActive(true);
        particles.SetActive(true);
    }

    public void StopFire()
    {
        isFiring = false;
        steamCollider.SetActive(false);
        particles.SetActive(false);
    }
}