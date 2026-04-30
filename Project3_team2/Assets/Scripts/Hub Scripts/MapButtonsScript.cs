using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    public string levelName;
    public Image image;

    public Color normalColor = Color.white;
    public Color selectedColor = Color.green;

    public void SelectLevel()
    {
        MapSystem.SetLevel(levelName);

        MapButton[] allButtons = Object.FindObjectsByType<MapButton>(FindObjectsSortMode.None);

        foreach (MapButton btn in allButtons)
        {
            btn.image.color = btn.normalColor;
        }

        image.color = selectedColor;
    }
}