using UnityEngine;

public class MapSystem : MonoBehaviour
{
    public static string selectedLevel;

    public static bool HasSelection()
    {
        return !string.IsNullOrEmpty(selectedLevel);
    }

    public static void SetLevel(string levelName)
    {
        selectedLevel = levelName;
        Debug.Log("Selected: " + levelName);
    }
}