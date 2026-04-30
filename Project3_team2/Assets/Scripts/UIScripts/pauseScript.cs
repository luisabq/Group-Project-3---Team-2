using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class pauseScript : MonoBehaviour
{
    private UIDocument _document;
    private VisualElement _root;

    private Button _backButton;
    private Button _settingsButton;
    private Button _menuButton;

    [Header("Gameplay References")]
    public PlayerMovement playerMovement;
    public ThirdPersonCam thirdPersonCam;

    private bool _isPaused = false;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _root = _document.rootVisualElement;

        _backButton = _root.Q<Button>("backButton");
        _settingsButton = _root.Q<Button>("settingsButton");
        _menuButton = _root.Q<Button>("menuButton");

        if (_backButton != null)
            _backButton.clicked += OnBackClicked;
        else
            Debug.LogWarning("backButton not found!");

        if (_settingsButton != null)
            _settingsButton.clicked += OnSettingsClicked;
        else
            Debug.LogWarning("settingsButton not found!");

        if (_menuButton != null)
            _menuButton.clicked += OnMenuClicked;
        else
            Debug.LogWarning("menuButton not found!");

        _root.style.display = DisplayStyle.None;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void PauseGame()
    {
        _isPaused = true;
        Time.timeScale = 0f;
        _root.style.display = DisplayStyle.Flex;

        if (playerMovement != null)
            playerMovement.enabled = false;

        if (thirdPersonCam != null)
            thirdPersonCam.enabled = false;

        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }

    private void ResumeGame()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        _root.style.display = DisplayStyle.None;

        if (playerMovement != null)
            playerMovement.enabled = true;

        if (thirdPersonCam != null)
            thirdPersonCam.enabled = true;

        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnBackClicked()
    {
        Debug.Log("Back clicked");
        ResumeGame();
    }

    private void OnSettingsClicked()
    {
        Debug.Log("Settings clicked");
    }

    private void OnMenuClicked()
    {
        Debug.Log("Menu clicked");

        Time.timeScale = 1f;
        _isPaused = false;

        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("MainMenu");
    }

    private void OnDisable()
    {
        if (_backButton != null)
            _backButton.clicked -= OnBackClicked;

        if (_settingsButton != null)
            _settingsButton.clicked -= OnSettingsClicked;

        if (_menuButton != null)
            _menuButton.clicked -= OnMenuClicked;
    }
}