using UnityEngine;

public class MapTable : MonoBehaviour, IInteractable
{
    public GameObject mapUI;

    public void Interact()
    {
        mapUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}