using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    [Header("Ambience Clips")]
    public AudioClip[] ambienceClips;

    [Header("Audio Source")]
    public AudioSource audioSource;

    private int currentClipIndex = 0;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        PlayNextClip();
    }

    void Update()
    {
        if (audioSource == null)
            return;

        if (ambienceClips == null || ambienceClips.Length == 0)
            return;

        // If nothing is playing anymore, move to the next ambience clip.
        if (!audioSource.isPlaying)
        {
            PlayNextClip();
        }
    }

    void PlayNextClip()
    {
        if (audioSource == null)
            return;

        if (ambienceClips == null || ambienceClips.Length == 0)
            return;

        AudioClip nextClip = ambienceClips[currentClipIndex];

        currentClipIndex++;

        if (currentClipIndex >= ambienceClips.Length)
        {
            currentClipIndex = 0;
        }

        audioSource.clip = nextClip;
        audioSource.Play();
    }
}