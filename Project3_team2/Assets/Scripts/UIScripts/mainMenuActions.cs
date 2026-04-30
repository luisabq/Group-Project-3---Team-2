using System.Collections.Generic;
using System.Linq;
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

        if (_playButton != null)
            _playButton.RegisterCallback<ClickEvent>(OnPlayButtonClick);
        else
            Debug.LogWarning("playButton not found!");

        if (_howToPlayButton != null)
            _howToPlayButton.RegisterCallback<ClickEvent>(OnHowToPlayButtonClick);
        else
            Debug.LogWarning("howtoplayButton not found!");

        if (_quitButton != null)
            _quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClick);
        else
            Debug.LogWarning("quitButton not found!");

        if (_creditButton != null)
            _creditButton.RegisterCallback<ClickEvent>(OnCreditButtonClick);
        else
            Debug.LogWarning("creditButton not found!");

        FocusButton(_playButton);
    }

    private void OnDisable()
    {
        if (_playButton != null)
            _playButton.UnregisterCallback<ClickEvent>(OnPlayButtonClick);

        if (_howToPlayButton != null)
            _howToPlayButton.UnregisterCallback<ClickEvent>(OnHowToPlayButtonClick);

        if (_quitButton != null)
            _quitButton.UnregisterCallback<ClickEvent>(OnQuitButtonClick);

        if (_creditButton != null)
            _creditButton.UnregisterCallback<ClickEvent>(OnCreditButtonClick);
    }

    private void OnPlayButtonClick(ClickEvent evt)
    {
        SceneManager.LoadScene("Hub Scene");
    }

    private void OnHowToPlayButtonClick(ClickEvent evt)
    {
        SceneManager.LoadScene("HowToPlay");
    }

    private void OnQuitButtonClick(ClickEvent evt)
    {
        Application.Quit();
    }

    private void OnCreditButtonClick(ClickEvent evt)
    {
        SceneManager.LoadScene("Credits");
    }

    private void FocusButton(Button button)
    {
        if (button != null)
            button.schedule.Execute(() => button.Focus());
    }
}
