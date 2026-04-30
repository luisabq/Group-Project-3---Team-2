using UnityEngine;

public class TreasureChest : MonoBehaviour, IInteractable
{
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

    void OpenChest()
    {
        isOpened = true;

        Time.timeScale = 0f;
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