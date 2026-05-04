using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class mainMenuActions : MonoBehaviour
{
    private UIDocument _document;
    private VisualElement _root;

    private Button _playButton;
    private Button _howToPlayButton;
    private Button _quitButton;
    private Button _creditButton;

    private List<Button> _buttons;
    private int _currentIndex = 0;

    private float inputCooldown = 0.2f;
    private float lastInputTime = 0f;

    bool usingController = true;

    private void Awake()
    {

        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

        _document = GetComponent<UIDocument>();
        _root = _document.rootVisualElement;

        _playButton = _root.Q<Button>("playButton");
        _howToPlayButton = _root.Q<Button>("howtoplayButton");
        _quitButton = _root.Q<Button>("quitButton");
        _creditButton = _root.Q<Button>("creditButton");

        Debug.Log(_playButton);
        Debug.Log(_howToPlayButton);
        Debug.Log(_quitButton);
        Debug.Log(_creditButton);

        _buttons = new List<Button>()
        {
            _playButton,
            _howToPlayButton,
            _quitButton,
            _creditButton
        };

        _currentIndex = 0;
        _buttons[_currentIndex].AddToClassList("selected");
        FocusButton(_buttons[_currentIndex]);

        if (_playButton != null)
            _playButton.RegisterCallback<ClickEvent>(evt => OnPlay());

        if (_howToPlayButton != null)
            _howToPlayButton.RegisterCallback<ClickEvent>(evt => OnHowToPlay());

        if (_quitButton != null)
            _quitButton.RegisterCallback<ClickEvent>(evt => OnQuit());

        if (_creditButton != null)
            _creditButton.RegisterCallback<ClickEvent>(evt => OnCredits());
    }

    void Update()
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
            Debug.Log("Clicking: " + _buttons[_currentIndex].name);

            switch (_currentIndex)
            {
                case 0: OnPlay(); break;
                case 1: OnHowToPlay(); break;
                case 2: OnQuit(); break;
                case 3: OnCredits(); break;
            }
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

    private void FocusButton(Button button)
    {
        if (button != null)
        {
            button.Focus();
        }
    }

    private void OnPlay()
    {
        Debug.Log("Play clicked");
        SceneManager.LoadScene("CutsceneIntro");
    }

    private void OnHowToPlay()
    {
        Debug.Log("How To Play clicked");
        SceneManager.LoadScene("HowToPlay");
    }

    private void OnQuit()
    {
        Debug.Log("Quit clicked");
        Application.Quit();
    }

    private void OnCredits()
    {
        Debug.Log("Credits clicked");
        SceneManager.LoadScene("Credits");
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