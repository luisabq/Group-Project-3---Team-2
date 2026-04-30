using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class winScript : MonoBehaviour
{
    private UIDocument _document;
    private Button _restartButton;
    private Button _menuButton;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _restartButton = _document.rootVisualElement.Q<Button>("restartButton");
        _menuButton = _document.rootVisualElement.Q<Button>("menuButton");

        if (_restartButton != null)
            _restartButton.RegisterCallback<ClickEvent>(OnRestartButtonClick);
        else
            Debug.LogWarning("restartButton not found!");

        if (_menuButton != null)
            _menuButton.RegisterCallback<ClickEvent>(OnMenuButtonClick);
        else
            Debug.LogWarning("menuButton not found!");
    }

    private void OnDisable()
    {
        if (_restartButton != null)
            _restartButton.UnregisterCallback<ClickEvent>(OnRestartButtonClick);

        if (_menuButton != null)
            _menuButton.UnregisterCallback<ClickEvent>(OnMenuButtonClick);
    }

    private void OnRestartButtonClick(ClickEvent evt)
    {
        Debug.Log("Restart clicked");
        SceneManager.LoadScene("Ruin Level");
    }

    private void OnMenuButtonClick(ClickEvent evt)
    {
        Debug.Log("Main Menu clicked");
        SceneManager.LoadScene("MainMenu");
    }
}