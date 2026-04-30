using System.Collections;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioSource currentMusic;
    public AudioSource newMusic;
    public float newMusicStartTime = 0f;
    public float fadeOutDuration = 2f;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(FadeOutAndSwitch());
        }
    }

    IEnumerator FadeOutAndSwitch()
    {
        float startVolume = currentMusic.volume;
        float time = 0f;

        while (time < fadeOutDuration)
        {
            time += Time.deltaTime;
            currentMusic.volume = Mathf.Lerp(startVolume, 0f, time / fadeOutDuration);
            yield return null;
        }

        currentMusic.volume = 0f;
        currentMusic.Stop();

        newMusic.gameObject.SetActive(true);
        newMusic.time = newMusicStartTime;
        newMusic.Play();
    }
}