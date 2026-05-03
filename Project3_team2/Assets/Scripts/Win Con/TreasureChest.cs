using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TreasureChest : MonoBehaviour, IInteractable
{
    public string endingSceneName = "CutsceneEnding";

    [Header("UI")]
    public GameObject lockedText;

    private bool isOpened = false;

    public void Interact()
    {
        if (isOpened) return;

        if (GameProgress.Instance != null && GameProgress.Instance.HasAllKeys())
        {
            OpenChest();
        }
        else
        {
            LockedFeedback();
        }
    }

    IEnumerator LoadEndingScene()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(endingSceneName);
    }

    void OpenChest()
    {
        isOpened = true;

        Debug.Log("YOU WIN!");

        StartCoroutine(LoadEndingScene());
    }

    void LockedFeedback()
    {
        Debug.Log("chest is locked");

        if (lockedText != null)
        {
            lockedText.SetActive(true);
            Invoke(nameof(HideLockedText), 2f);
        }
    }

    void HideLockedText()
    {
        if (lockedText != null)
        {
            lockedText.SetActive(false);
        }
    }
}