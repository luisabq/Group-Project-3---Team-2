using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class mainMenuActions : MonoBehaviour
{
    private UIDocument _document;
    private Button _playButton;
    private Button _quitButton;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _playButton = _document.rootVisualElement.Q<Button>("playButton");
        _quitButton = _document.rootVisualElement.Q<Button>("quitButton");

        if (_playButton != null)
        {
            _playButton.RegisterCallback<ClickEvent>(OnPlayButtonClick);
        }
        else
        {
            Debug.LogWarning("playButton not found!");
        }

        if (_quitButton != null)
        {
            _quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClick);
        }
        else
        {
            Debug.LogWarning("quitButton not found!");
        }
    }

    private void OnDisable()
    {
        if (_playButton != null)
        {
            _playButton.UnregisterCallback<ClickEvent>(OnPlayButtonClick);
        }

        if (_quitButton != null)
        {
            _quitButton.UnregisterCallback<ClickEvent>(OnQuitButtonClick);
        }
    }

    private void OnPlayButtonClick(ClickEvent evt)
    {
        Debug.Log("Play button clicked");

        // Load your scene
        SceneManager.LoadScene("Hub Scene");
    }

    private void OnQuitButtonClick(ClickEvent evt)
    {
        Debug.Log("Quit button clicked");

        Application.Quit();
    }
}