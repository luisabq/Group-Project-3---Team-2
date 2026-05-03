using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class pauseScript : MonoBehaviour
{
    private UIDocument _document;
    private VisualElement _root;
    private UIDocument _controlsDocumentInstance;
    private VisualElement _controlsRoot;

    private Button _backButton;
    private Button _restartButton;
    private Button _menuButton;
    private Button _backButtonControls;

    [Header("Reset Reference")]
    private Reset resetScript;

    [Header("Gameplay References")]
    public PlayerMovement playerMovement;
    public ThirdPersonCam thirdPersonCam;
    public PlayerToolController toolController;

    [Header("UI References")]
    public UIDocument controlsDocument;

    private bool _isPaused = false;

    private void Awake()
    {
        resetScript = FindFirstObjectByType<Reset>();

        _document = GetComponent<UIDocument>();
        _root = _document.rootVisualElement;

        _backButton = _root.Q<Button>("backButton");
        _restartButton = _root.Q<Button>("restartButton");
        _menuButton = _root.Q<Button>("menuButton");

        if (_backButton != null)
            _backButton.clicked += OnBackClicked;
        else
            Debug.LogWarning("backButton not found!");

        if (_restartButton != null)
            _restartButton.clicked += OnRestartClicked;
        else
            Debug.LogWarning("restartButton not found!");

        if (_menuButton != null)
            _menuButton.clicked += OnMenuClicked;
        else
            Debug.LogWarning("menuButton not found!");

        SetupControlsDocument();
        _root.style.display = DisplayStyle.None;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            if (MapTable.IsMapOpen)
            {
                MapTable mapTable = Object.FindFirstObjectByType<MapTable>();
                if (mapTable != null)
                {
                    mapTable.CloseMap();
                }
                return;
            }

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

        if (toolController != null)
            toolController.enabled = false;

        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }

    private void ResumeGame()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        _root.style.display = DisplayStyle.None;
        HideControlsOverlay();

        if (!MapTable.IsMapOpen)
        {
            if (playerMovement != null)
                playerMovement.enabled = true;

            if (thirdPersonCam != null)
                thirdPersonCam.enabled = true;

            if (toolController != null)
                toolController.enabled = true;
        }

        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnBackClicked()
    {
        Debug.Log("Back clicked");
        ResumeGame();
    }

    private void OnRestartClicked()
    {
        Debug.Log("Restart clicked");

        Debug.Log("Reset reference: " + resetScript);

        if (resetScript != null)
        {
            resetScript.Drop();
        }
        else
        {
            Debug.LogError("RESET IS NULL AT RUNTIME");
        }

        ResumeGame();
    }

    private void OnMenuClicked()
    {
        Debug.Log("Menu clicked");

        Time.timeScale = 1f;
        _isPaused = false;
        HideControlsOverlay();

        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("MainMenu");
    }

    private void OnDisable()
    {
        if (_backButton != null)
            _backButton.clicked -= OnBackClicked;

        if (_restartButton != null)
            _restartButton.clicked -= OnRestartClicked;

        if (_menuButton != null)
            _menuButton.clicked -= OnMenuClicked;

        if (_backButtonControls != null)
            _backButtonControls.clicked -= OnControlsBackClicked;
    }

    private void SetupControlsDocument()
    {
        _controlsDocumentInstance = controlsDocument != null ? controlsDocument : FindControlsDocument();

        if (_controlsDocumentInstance == null)
        {
            Debug.LogWarning("Controls UIDocument not found!");
            return;
        }

        _controlsDocumentInstance.gameObject.SetActive(false);
    }

    private UIDocument FindControlsDocument()
    {
        UIDocument[] documents = Resources.FindObjectsOfTypeAll<UIDocument>();

        foreach (UIDocument document in documents)
        {
            if (document == null || document == _document)
                continue;

            if (!document.gameObject.scene.IsValid())
                continue;

            if (document.gameObject.name == "Controls")
                return document;
        }

        return null;
    }

    private void ShowControlsOverlay()
    {
        if (_controlsDocumentInstance == null)
            return;

        _controlsDocumentInstance.gameObject.SetActive(true);
        _controlsRoot = _controlsDocumentInstance.rootVisualElement;

        if (_controlsRoot == null)
        {
            Debug.LogWarning("Controls root visual element not found!");
            return;
        }

        _controlsRoot.style.display = DisplayStyle.Flex;
        BindControlsBackButton();
    }

    private void OnControlsBackClicked()
    {
        _root.style.display = DisplayStyle.Flex;
        HideControlsOverlay();
    }

    private void HideControlsOverlay()
    {
        if (_controlsRoot != null)
            _controlsRoot.style.display = DisplayStyle.None;

        if (_controlsDocumentInstance != null)
            _controlsDocumentInstance.gameObject.SetActive(false);
    }

    private void BindControlsBackButton()
    {
        if (_backButtonControls != null)
            _backButtonControls.clicked -= OnControlsBackClicked;

        _backButtonControls = _controlsRoot.Q<Button>("backButtonControls");

        if (_backButtonControls != null)
            _backButtonControls.clicked += OnControlsBackClicked;
        else
            Debug.LogWarning("backButtonControls not found!");
    }
}
