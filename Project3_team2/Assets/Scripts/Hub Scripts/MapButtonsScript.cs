using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapButton : MonoBehaviour
{
    public string levelName;
    public Image image;

    public Color normalColor = Color.white;
    public Color selectedColor = Color.green;
    public Color completedColor = Color.red;

    public Color hoverColor = Color.yellow;

    private bool isCompleted = false;

    public int levelIndex;

    public MapTable mapTable;

    void Start()
    {
        // check for completion
        if (GameProgress.Instance != null &&
            GameProgress.Instance.completedLevels.Contains(levelName))
        {
            isCompleted = true;
            image.color = completedColor;
        }
    }

    public void SelectLevel()
    {
        // block it once completed
        if (isCompleted)
        {
            Debug.Log("Level already completed!");
            return;
        }

        MapSystem.SetLevel(levelName);

        if (mapTable != null)
        {
            mapTable.ShowLevelInfo(levelIndex);
        }

        MapButton[] allButtons = Object.FindObjectsByType<MapButton>(FindObjectsSortMode.None);

        foreach (MapButton btn in allButtons)
        {
            // if completed make red
            if (!btn.isCompleted)
            {
                btn.image.color = btn.normalColor;
            }
        }

        image.color = selectedColor;
    }
    void Update()
    {
        bool isFocused = EventSystem.current.currentSelectedGameObject == gameObject;

        if (isFocused)
        {
            if (mapTable != null)
            {
                mapTable.ShowLevelInfo(levelIndex);
            }

            if (!isCompleted)
            {
                image.color = hoverColor;
            }

            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                SelectLevel();
            }
        }
        else
        {
  
            if (!isCompleted)
            {
                image.color = normalColor;
            }
        }
    }
}