using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class mainMenuActions : MonoBehaviour
{
    private UIDocument _document;

    private Button _playButton;
    private Button _howToPlayButton;
    private Button _quitButton;
    private Button _creditButton;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _playButton = _document.rootVisualElement.Q<Button>("playButton");
        _howToPlayButton = _document.rootVisualElement.Q<Button>("howtoplayButton");
        _quitButton = _document.rootVisualElement.Q<Button>("quitButton");
        _creditButton = _document.rootVisualElement.Q<Button>("creditButton");

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
        Debug.Log("Play clicked");
        SceneManager.LoadScene("Hub Scene");
    }

    private void OnHowToPlayButtonClick(ClickEvent evt)
    {
        Debug.Log("How To Play clicked");
        SceneManager.LoadScene("HowToPlay");
    }

    private void OnQuitButtonClick(ClickEvent evt)
    {
        Debug.Log("Quit clicked");
        Application.Quit();
    }

    private void OnCreditButtonClick(ClickEvent evt)
    {
        Debug.Log("Credits clicked");
        SceneManager.LoadScene("Credits");
    }
}