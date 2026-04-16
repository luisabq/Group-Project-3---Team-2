using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class pauseScript : MonoBehaviour
{
    private UIDocument _document;

    private Button _backButton;
    private Button _settingsButton;
    private Button _quitButton;

    private bool _isPaused = false;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _backButton = _document.rootVisualElement.Q<Button>("backButton");
        _settingsButton = _document.rootVisualElement.Q<Button>("settingsButton");
        _quitButton = _document.rootVisualElement.Q<Button>("quitButton");

        if (_backButton != null)
            _backButton.RegisterCallback<ClickEvent>(OnBackClicked);
        else
            Debug.LogWarning("backButton not found!");

        if (_settingsButton != null)
            _settingsButton.RegisterCallback<ClickEvent>(OnSettingsClicked);
        else
            Debug.LogWarning("settingsButton not found!");

        if (_quitButton != null)
            _quitButton.RegisterCallback<ClickEvent>(OnQuitClicked);
        else
            Debug.LogWarning("quitButton not found!");

        gameObject.SetActive(false);
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

    private void OnDisable()
    {
        if (_backButton != null)
            _backButton.UnregisterCallback<ClickEvent>(OnBackClicked);

        if (_settingsButton != null)
            _settingsButton.UnregisterCallback<ClickEvent>(OnSettingsClicked);

        if (_quitButton != null)
            _quitButton.UnregisterCallback<ClickEvent>(OnQuitClicked);
    }

    private void PauseGame()
    {
        _isPaused = true;
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    private void ResumeGame()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    private void OnBackClicked(ClickEvent evt)
    {
        Debug.Log("Resume clicked");
        ResumeGame();
    }

    private void OnSettingsClicked(ClickEvent evt)
    {
        Debug.Log("Settings clicked");
        // Add settings logic here later
    }

    private void OnQuitClicked(ClickEvent evt)
    {
        Debug.Log("Quit to menu clicked");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}