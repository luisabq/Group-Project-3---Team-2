using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class winScript : MonoBehaviour
{
    private UIDocument _document;
    private VisualElement _root;

    private Button _restartButton;
    private Button _menuButton;

    private List<Button> _buttons;
    private int _currentIndex = 0;

    private float inputCooldown = 0.2f;
    private float lastInputTime = 0f;

    bool usingController = true;

    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _root = _document.rootVisualElement;

        _restartButton = _root.Q<Button>("restartButton");
        _menuButton = _root.Q<Button>("menuButton");

        if (_restartButton != null)
            _restartButton.RegisterCallback<ClickEvent>(evt => OnRestart());

        if (_menuButton != null)
            _menuButton.RegisterCallback<ClickEvent>(evt => OnMenu());

        _buttons = new List<Button>()
        {
            _restartButton,
            _menuButton
        };

        _currentIndex = 0;
        _buttons[_currentIndex].AddToClassList("selected");
        FocusButton(_buttons[_currentIndex]);
    }

    private void Update()
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

        if (Time.time - lastInputTime > inputCooldown)
        {
            if (vertical > 0.5f)
            {
                MoveSelection(-1);
                lastInputTime = Time.time;
            }
            else if (vertical < -0.5f)
            {
                MoveSelection(1);
                lastInputTime = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            ActivateCurrentButton();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            OnMenu();
        }
    }

    void MoveSelection(int direction)
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

        FocusButton(_buttons[_currentIndex]);
    }

    void ActivateCurrentButton()
    {
        switch (_currentIndex)
        {
            case 0: OnRestart(); break;
            case 1: OnMenu(); break;
        }
    }

    private void OnRestart()
    {
        Debug.Log("Restart clicked");
        SceneManager.LoadScene("MainMenu");
    }

    private void OnMenu()
    {
        Debug.Log("Main Menu clicked");
        SceneManager.LoadScene("MainMenu");
    }

    private void FocusButton(Button button)
    {
        if (button != null)
            button.Focus();
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
}