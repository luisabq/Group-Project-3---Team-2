using UnityEngine;
using UnityEngine.UIElements;

public class hudScript : MonoBehaviour
{
    private UIDocument _document;

    private VisualElement _outIcon;
    private VisualElement _grappleIcon;
    private VisualElement _steamIcon;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _outIcon = _document.rootVisualElement.Q<VisualElement>("out");
        _grappleIcon = _document.rootVisualElement.Q<VisualElement>("grapple");
        _steamIcon = _document.rootVisualElement.Q<VisualElement>("steam");

        if (_outIcon == null)
            Debug.LogWarning("out icon not found!");

        if (_grappleIcon == null)
            Debug.LogWarning("grapple icon not found!");

        if (_steamIcon == null)
            Debug.LogWarning("steam icon not found!");
    }

    public void ShowOut(bool show)
    {
        if (_outIcon != null)
            _outIcon.style.display = show ? DisplayStyle.Flex : DisplayStyle.None;
    }

    public void ShowGrapple(bool show)
    {
        if (_grappleIcon != null)
            _grappleIcon.style.display = show ? DisplayStyle.Flex : DisplayStyle.None;
    }

    public void ShowSteam(bool show)
    {
        if (_steamIcon != null)
            _steamIcon.style.display = show ? DisplayStyle.Flex : DisplayStyle.None;
    }
}