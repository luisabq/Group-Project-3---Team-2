using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 3f;
    public Camera cam;

    [Header("UI")]
    public GameObject interactText;

    private IInteractable currentInteractable;

    void Update()
    {
        CheckForInteractable();

        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void CheckForInteractable()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        int layerMask = ~LayerMask.GetMask("Player");

        if (Physics.Raycast(ray, out hit, interactRange, layerMask))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable == null)
            {
                interactable = hit.collider.GetComponentInParent<IInteractable>();
            }

            if (interactable != null)
            {
                currentInteractable = interactable;
                interactText.SetActive(true);
                return;
            }
        }

        currentInteractable = null;
        interactText.SetActive(false);
    }
}