using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class KeyInteract : MonoBehaviour, IInteractable
{
    [Header("Effects")]
    public AudioClip pickupSound;
    public GameObject pickupEffect;

    [Header("UI")]
    public GameObject collectedText;

    [Header("Scene")]
    public string hubSceneName = "Hub Scene";

    private Renderer[] renderers;
    private Collider[] colliders;

    void Awake()
    {

        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();
    }

    public void Interact()
    {
        StartCoroutine(CollectKey());
    }

    IEnumerator CollectKey()
    {

        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }

        if (pickupEffect != null)
        {
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
        }

        if (collectedText != null)
        {
            collectedText.SetActive(true);
        }

        foreach (Renderer r in renderers)
        {
            r.enabled = false;
        }

        foreach (Collider c in colliders)
        {
            c.enabled = false;
        }

        yield return new WaitForSecondsRealtime(1.5f);

        GameProgress.Instance.AddKey();

        GameProgress.Instance.CompleteLevel(SceneManager.GetActiveScene().name);

        MapSystem.selectedLevel = "";

        SceneManager.LoadScene(hubSceneName);
    }
}