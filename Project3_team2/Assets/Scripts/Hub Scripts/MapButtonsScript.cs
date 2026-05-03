using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    public string levelName;
    public Image image;

    public Color normalColor = Color.white;
    public Color selectedColor = Color.green;
    public Color completedColor = Color.red;

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
}