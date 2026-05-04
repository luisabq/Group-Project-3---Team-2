using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 3f;
    public float interactRadius = 0.5f;

    [Header("UI")]
    public GameObject interactText;

    public void HidePrompt()
    {
        if (interactText != null)
            interactText.SetActive(false);
    }

    private IInteractable currentInteractable;

    void Update()
    {
        // stop interaction while paused
        if (Time.timeScale == 0f) return;

        CheckForInteractable();

        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton2)) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void CheckForInteractable()
    {
        Camera cam = Camera.main;

        if (cam == null)
        {
            Debug.LogWarning("No Main Camera found!");
            return;
        }

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        int layerMask = ~LayerMask.GetMask("Player");

        if (Physics.SphereCast(ray, interactRadius, out hit, interactRange, layerMask))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable == null)
                interactable = hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                currentInteractable = interactable;

                if (interactText != null)
                    interactText.SetActive(true);

                return;
            }
        }

        currentInteractable = null;

        if (interactText != null)
            interactText.SetActive(false);
    }
}