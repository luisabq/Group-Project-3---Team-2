using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource windSource;
    public AudioSource droneSource;
    public AudioSource drumSource;
    public AudioSource hornSource;

    [Header("Volumes")]
    public float drumVolume = 0.3f;
    public float hornVolume = 0.5f;

    private bool drumsPending = false;
    private bool drumsPlaying = false;

    private bool hornQueued = false;

    void Start()
    {
        // wind and drone play
        windSource.Play();
        droneSource.loop = true;
        droneSource.Play();

        // loop counting
        StartCoroutine(DroneLoopWatcher());

        // random horns
        StartCoroutine(RandomHornQueue());
    }

    // enter canyon
    public void EnterCanyon()
    {
        drumsPending = true;
    }

    // drone loops
    IEnumerator DroneLoopWatcher()
    {
        float lastTime = 0f;

        while (true)
        {
            float currentTime = droneSource.time;

            // reset time
            if (currentTime < lastTime)
            {
                OnDroneLoop();
            }

            lastTime = currentTime;

            yield return null;
        }
    }

    void OnDroneLoop()
    {
        // start drums when loop
        if (drumsPending && !drumsPlaying)
        {
            drumSource.volume = drumVolume;
            drumSource.loop = true;
            drumSource.Play();

            drumsPlaying = true;
            drumsPending = false;
        }

        // play horns
        if (hornQueued && drumsPlaying)
        {
            hornSource.PlayOneShot(hornSource.clip, hornVolume);
            hornQueued = false;
        }
    }

    // randomize horns
    IEnumerator RandomHornQueue()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(15f, 40f));

            if (drumsPlaying)
            {
                hornQueued = true;
            }
        }
    }
}