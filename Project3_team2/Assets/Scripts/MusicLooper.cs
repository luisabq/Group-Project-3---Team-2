using UnityEngine;

public class MusicLooper : MonoBehaviour
{

    public AudioSource audioSource;
    public float loopStart = 16f; 
    public float loopEnd = 127.5f; 
    
    void Update()
    {
     if (audioSource.isPlaying && audioSource.time >= loopEnd)
        {
            audioSource.time = loopStart;
        }   
    }
}
