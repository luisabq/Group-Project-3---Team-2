using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public static GameProgress Instance;

    public int keysCollected = 0;

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

    public bool HasAllKeys()
    {
        return keysCollected >= 3;
    }
}
