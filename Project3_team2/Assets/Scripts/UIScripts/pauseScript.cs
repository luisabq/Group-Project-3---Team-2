using System.Collections.Generic;
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

    private List<Button> _buttons;
    private int _currentIndex = 0;

    private float inputCooldown = 0.2f;
    private float lastInputTime = 0f;

    bool usingController = true;

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

        if (_restartButton != null)
            _restartButton.clicked += OnRestartClicked;

        if (_menuButton != null)
            _menuButton.clicked += OnMenuClicked;

        _buttons = new List<Button>()
        {
            _backButton,
            _restartButton,
            _menuButton
        };

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
                    return;
                }
                else
                {
                    MapTable.IsMapOpen = false;
                }
            }

            if (_isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        if (_isPaused)
        {

            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                usingController = false;
                ClearSelectionHighlight();
            }

            if (Input.GetMouseButtonDown(0))
            {
                usingController = false;
                ClearSelectionHighlight();
            }

            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                usingController = false;
                ClearSelectionHighlight();
            }

            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f ||
                Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                if (!usingController)
                {
                    usingController = true;
                    ApplySelectionHighlight();
                }
            }

            float vertical = Input.GetAxis("Vertical");

            if (Time.unscaledTime - lastInputTime > inputCooldown)
            {
                if (vertical > 0.5f)
                {
                    MoveSelection(-1);
                    lastInputTime = Time.unscaledTime;
                }
                else if (vertical < -0.5f)
                {
                    MoveSelection(1);
                    lastInputTime = Time.unscaledTime;
                }
            }

            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                ActivateCurrentButton();
            }
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

        _currentIndex = 0;

        foreach (var btn in _buttons)
            btn.RemoveFromClassList("selected");

        _buttons[_currentIndex].AddToClassList("selected");
        _buttons[_currentIndex].Focus();
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
        ResumeGame();
    }

    private void OnRestartClicked()
    {
        if (resetScript != null)
        {
            resetScript.Drop();
        }

        ResumeGame();
    }

    private void OnMenuClicked()
    {
        Time.timeScale = 1f;
        _isPaused = false;
        HideControlsOverlay();

        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("MainMenu");
    }

    private void MoveSelection(int direction)
    {
        if (usingController)
            _buttons[_currentIndex].RemoveFromClassList("selected");

        _currentIndex += direction;

        if (_currentIndex < 0)
            _currentIndex = _buttons.Count - 1;

        if (_currentIndex >= _buttons.Count)
            _currentIndex = 0;

        if (usingController)
            _buttons[_currentIndex].AddToClassList("selected");

        _buttons[_currentIndex].Focus();
    }

    private void ActivateCurrentButton()
    {
        switch (_currentIndex)
        {
            case 0: OnBackClicked(); break;
            case 1: OnRestartClicked(); break;
            case 2: OnMenuClicked(); break;
        }
    }

    void ClearSelectionHighlight()
    {
        foreach (var btn in _buttons)
        {
            btn.RemoveFromClassList("selected");
        }
    }

    void ApplySelectionHighlight()
    {
        _buttons[_currentIndex].AddToClassList("selected");
    }

    private void SetupControlsDocument()
    {
        _controlsDocumentInstance = controlsDocument != null ? controlsDocument : FindControlsDocument();

        if (_controlsDocumentInstance == null)
            return;

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
            return;

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
    }
}
