using UnityEngine;

public class MapTable : MonoBehaviour, IInteractable
{
    public GameObject mapUI;

    [Header("Disable While Open")]
    public PlayerMovement playerMovement;
    public PlayerToolController toolController;
    public PlayerInteract playerInteract;
    public static bool IsMapOpen = false;

    public void Interact()
    {
        bool enginesReady = playerMovement.activeSteamReceptors >= 2;

        if (GameProgress.Instance != null && GameProgress.Instance.hubEnginesActivated)
        {
            enginesReady = true;
        }

        if (!enginesReady)
        {
            Debug.Log("Power the engines first!");
            return;
        }

        mapUI.SetActive(true);
        IsMapOpen = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //no more moving :D
        if (playerMovement != null) playerMovement.enabled = false;
        if (toolController != null) toolController.enabled = false;
        if (playerInteract != null)
        {
            playerInteract.HidePrompt();
            playerInteract.enabled = false;
        }

        Time.timeScale = 0f;
    }
    public void CloseMap()
    {
        mapUI.SetActive(false);
        IsMapOpen = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerMovement != null) playerMovement.enabled = true;
        if (toolController != null) toolController.enabled = true;
        if (playerInteract != null) playerInteract.enabled = true;

        Time.timeScale = 1f;
    }
}