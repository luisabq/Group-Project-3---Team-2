using UnityEngine;
using UnityEngine.UIElements;

public class hudScript : MonoBehaviour
{
    private UIDocument _document;
    private VisualElement _root;

    private VisualElement _selectedAction;

    [Header("Action Icons")]
    public Texture2D outIcon;
    public Texture2D grappleIcon;
    public Texture2D steamIcon;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _root = _document.rootVisualElement;

        // HUD should not block mouse input
        _root.pickingMode = PickingMode.Ignore;

        // NEW NAME
        _selectedAction = _root.Q<VisualElement>("selectedAction");

        if (_selectedAction == null)
            Debug.LogWarning("selectedAction not found!");
    }

    // 🔹 Set icon directly
    public void SetAction(Texture2D icon)
    {
        if (_selectedAction != null)
            _selectedAction.style.backgroundImage = new StyleBackground(icon);
    }

    // 🔹 Helper functions (cleaner to call from gameplay scripts)

    public void ShowOut()
    {
        SetAction(outIcon);
    }

    public void ShowGrapple()
    {
        SetAction(grappleIcon);
    }

    public void ShowSteam()
    {
        SetAction(steamIcon);
    }
}