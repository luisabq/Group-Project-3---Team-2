using UnityEngine;

public class MapTable : MonoBehaviour, IInteractable
{
    public GameObject mapUI;

    [Header("Disable While Open")]
    public PlayerMovement playerMovement;
    public PlayerToolController toolController;
    public PlayerInteract playerInteract;

    public void Interact()
    {
        mapUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // no more moving :D
        if (playerMovement != null) playerMovement.enabled = false;
        if (toolController != null) toolController.enabled = false;
        if (playerInteract != null) playerInteract.enabled = false;

        Time.timeScale = 0f;
    }
}