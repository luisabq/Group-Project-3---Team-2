using UnityEngine;
using UnityEngine.UIElements;

public class hudScript : MonoBehaviour
{
    public UIDocument uiDocument;
    private VisualElement selectedAction;

    public Texture2D grappleIcon;
    public Texture2D steamIcon;

    private void Start()
    {
        var root = uiDocument.rootVisualElement;
        selectedAction = root.Q<VisualElement>("selectedAction");

        if (selectedAction == null)
        {
            Debug.LogError("selectedAction not found!");
            return;
        }

        // HUD should not block mouse (important for your pause menu issue earlier)
        root.pickingMode = PickingMode.Ignore;
    }

    private void Update()
    {
        // Press 1 → Grapple
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetAction(grappleIcon);
        }

        // Press 2 → Steam
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetAction(steamIcon);
        }
    }

    private void SetAction(Texture2D icon)
    {
        if (selectedAction == null) return;

        selectedAction.style.backgroundImage = new StyleBackground(icon);
        selectedAction.style.display = DisplayStyle.Flex;
    }
}