using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class winScript : MonoBehaviour
{
    private UIDocument _document;
    private Button _restartButton;
    private Button _menuButton;

    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

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

        FocusButton(_restartButton);
    }

    private void Update()
    {
        if (MenuButtonPressed())
            LoadMainMenu();
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
        SceneManager.LoadScene("MainMenu");
    }

    private void OnMenuButtonClick(ClickEvent evt)
    {
        Debug.Log("Main Menu clicked");
        LoadMainMenu();
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void FocusButton(Button button)
    {
        if (button != null)
            button.schedule.Execute(() => button.Focus());
    }

    private bool MenuButtonPressed()
    {
        return Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton1);
    }
}
