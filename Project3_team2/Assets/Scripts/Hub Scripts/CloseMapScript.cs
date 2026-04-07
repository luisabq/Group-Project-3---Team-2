using UnityEngine;

public class CloseMapButton : MonoBehaviour
{
    public GameObject mapUI;

    public void CloseMap()
    {
        mapUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}