using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class creditScript : MonoBehaviour
{
    private UIDocument _document;
    private Button _backButton;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _backButton = _document.rootVisualElement.Q<Button>("backButton");

        if (_backButton != null)
            _backButton.RegisterCallback<ClickEvent>(OnBackButtonClick);
        else
            Debug.LogWarning("backButton not found!");
    }

    private void OnDisable()
    {
        if (_backButton != null)
            _backButton.UnregisterCallback<ClickEvent>(OnBackButtonClick);
    }

    private void OnBackButtonClick(ClickEvent evt)
    {
        Debug.Log("Back clicked");
        SceneManager.LoadScene("MainMenu");
    }
}