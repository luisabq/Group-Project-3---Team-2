using UnityEngine;
using System.Collections.Generic;

public class GameProgress : MonoBehaviour
{
    public static GameProgress Instance;

    public bool hubEnginesActivated = false;
    public int hubEnginesCount = 0;

    public int keysCollected = 0;

    public HashSet<string> completedLevels = new HashSet<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddKey()
    {
        keysCollected++;
        Debug.Log("Keys: " + keysCollected);
    }
    public void CompleteLevel(string levelName)
    {
        completedLevels.Add(levelName);
        Debug.Log("Completed: " + levelName);
    }

    public bool HasAllKeys()
    {
        return keysCollected >= 3;
    }
}