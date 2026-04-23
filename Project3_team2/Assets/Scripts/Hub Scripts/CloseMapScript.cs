using UnityEngine;

public class CloseMapButton : MonoBehaviour
{
    public GameObject mapUI;

    [Header("Re-enable After Close")]
    public PlayerMovement playerMovement;
    public PlayerToolController toolController;
    public PlayerInteract playerInteract;

    public void CloseMap()
    {
        mapUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // you can move again :D
        if (playerMovement != null) playerMovement.enabled = true;
        if (toolController != null) toolController.enabled = true;
        if (playerInteract != null) playerInteract.enabled = true;

        Time.timeScale = 1f;
    }
}