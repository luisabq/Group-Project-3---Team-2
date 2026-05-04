using UnityEngine;
using UnityEngine.UIElements;

public class hudScript : MonoBehaviour
{
    public UIDocument uiDocument;
    private VisualElement selectedAction;

    public Texture2D grappleIcon;
    public Texture2D steamIcon;

    public PlayerToolController toolController;

    private PlayerToolController.Tool lastTool;

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

        UpdateHUD(toolController.currentTool);
        lastTool = toolController.currentTool;
    }

    private void Update()
    {
        
        if (toolController.currentTool != lastTool)
        {
            UpdateHUD(toolController.currentTool);
            lastTool = toolController.currentTool;
        }
    }

    void UpdateHUD(PlayerToolController.Tool tool)
    {
        switch (tool)
        {
            case PlayerToolController.Tool.Grapple:
                SetAction(grappleIcon);
                break;

            case PlayerToolController.Tool.Steam:
                SetAction(steamIcon);
                break;
        }
    }

    private void SetAction(Texture2D icon)
    {
        if (selectedAction == null) return;

        selectedAction.style.backgroundImage = new StyleBackground(icon);
        selectedAction.style.display = DisplayStyle.Flex;
    }
}