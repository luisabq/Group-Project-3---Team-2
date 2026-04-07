using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    //  public static bool isPaused = false;

    // public GameObject targetObject;
    


    // [Header("UI Reference")]
    // public GameObject pauseMenuUI;


    // void Start()
    // {
    //     // Make sure everything starts correctly
    //     targetObject.GetComponent<FPSController>().enabled = true;
    //     pauseMenuUI.SetActive(false);
    //     Time.timeScale = 1f;
    //     isPaused = false;

    //     // Lock cursor at start (for FPS-style games)
    //     Cursor.lockState = CursorLockMode.Locked;
    //     Cursor.visible = false;
    // }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
    //     {
    //         if (isPaused)
    //             Resume();
    //         else
    //             Pause();
    //     }
    // }

    // public void Resume()
    // {
    //     targetObject.GetComponent<FPSController>().enabled = true;
    //     pauseMenuUI.SetActive(false);
    //     Time.timeScale = 1f;
    //     isPaused = false;

    //     Cursor.lockState = CursorLockMode.Locked;
    //     Cursor.visible = false;
    //     //gameObject.Find("PlayerController").GetComponent<FPSController>.enabled = true;
        
    // }

    // void Pause()
    // {
    //     targetObject.GetComponent<FPSController>().enabled = false;
    //     pauseMenuUI.SetActive(true);
    //     Time.timeScale = 0f;
    //     isPaused = true;

    //     Cursor.lockState = CursorLockMode.None;
    //     Cursor.visible = true;
    //     // gameObject.Find("PlayerController").GetComponent<FPSController>.enabled = false;
        

    // }

    // public void LoadMenu(string sceneName)
    // {
    //     Time.timeScale = 1f;
    //     isPaused = false;
    //     SceneManager.LoadScene(sceneName);
    // }

    // public void QuitGame()
    // {
    //     Debug.Log("Quitting Game...");
    //     Application.Quit();
    // }
}