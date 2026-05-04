using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class KeyInteract : MonoBehaviour, IInteractable
{
    [Header("Effects")]
    public AudioSource pickupAudioSource;

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

        if (pickupAudioSource != null)
        {
            pickupAudioSource.Play();
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

        float waitTime = 1.5f;

        if (pickupAudioSource != null && pickupAudioSource.clip != null)
        {
            waitTime = pickupAudioSource.clip.length;
        }

        yield return new WaitForSecondsRealtime(waitTime);

        if (GameProgress.Instance == null)
        {
            Debug.LogWarning("GameProgress missing, creating one.");
            new GameObject("GameProgress").AddComponent<GameProgress>();
        }

        GameProgress.Instance.AddKey();
        GameProgress.Instance.CompleteLevel(SceneManager.GetActiveScene().name);

        MapSystem.selectedLevel = "";

        SceneManager.LoadScene(hubSceneName);
    }
}