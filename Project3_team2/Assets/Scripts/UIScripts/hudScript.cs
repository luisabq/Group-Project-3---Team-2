using UnityEngine;
using UnityEngine.UIElements;

public class hudScript : MonoBehaviour
{
    public UIDocument uiDocument;
    private VisualElement selectedAction;

    public Texture2D outIcon;
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

        root.pickingMode = PickingMode.Ignore;
    }

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

    public void HideAction()
    {
        if (selectedAction != null)
            selectedAction.style.display = DisplayStyle.None;
    }

    private void SetAction(Texture2D icon)
    {
        if (selectedAction == null) return;

        selectedAction.style.backgroundImage = new StyleBackground(icon);
        selectedAction.style.display = DisplayStyle.Flex;
    }
}