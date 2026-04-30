using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class howToPlayScript : MonoBehaviour
{
    private UIDocument _document;
    private Button _backButton;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _backButton = _document.rootVisualElement.Q<Button>("backButton");

        if (_backButton != null)
        {
            _backButton.RegisterCallback<ClickEvent>(OnBackClicked);
        }
        else
        {
            Debug.LogWarning("backButton not found!");
        }
    }

    private void OnDisable()
    {
        if (_backButton != null)
        {
            _backButton.UnregisterCallback<ClickEvent>(OnBackClicked);
        }
    }

    private void OnBackClicked(ClickEvent evt)
    {
        Debug.Log("Back to menu");

        SceneManager.LoadScene("MainMenu");
    }
}