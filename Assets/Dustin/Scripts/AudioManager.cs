using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Mixers")]
    public AudioMixer audioMixer; // Exposed params: "MusicVol", "SFXVol"

    [Header("Music Setup")]
    [Tooltip("Optional: music that starts automatically.")]
    public AudioClip defaultMusic;
    public bool playDefaultOnStart = true;

    [Tooltip("Optional: drag in multiple tracks here.")]
    public AudioClip[] musicTracks;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Apply saved volumes so sliders affect audio immediately
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        SetMusicVolume(musicVol);
        SetSFXVolume(sfxVol);

        // Optionally, this starts the default music
        // (Only if nothing is already playing to avoid restarts between menu-like scenes)
        if (playDefaultOnStart && defaultMusic != null && !musicSource.isPlaying)
        {
            PlayMusic(defaultMusic, true);
        }
    }

    // -------- Music controls --------
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    // Helper: play only if the clip is different (prevents audible restarts)
    public void PlayMusicIfChanged(AudioClip clip, bool loop = true)
    {
        if (clip == null) return;
        if (musicSource.isPlaying && musicSource.clip == clip) return; // same track -> keep seamless
        PlayMusic(clip, loop);
    }

    // Helper: crossfade to a new clip (simple fade-out/in on the AudioSource volume)
    public void CrossfadeTo(AudioClip clip, float fadeTime = 0.75f, bool loop = true)
    {
        if (clip == null) return;
        if (musicSource.isPlaying && musicSource.clip == clip) return; // already on this track
        StartCoroutine(CrossfadeRoutine(clip, fadeTime, loop));
    }

    private System.Collections.IEnumerator CrossfadeRoutine(AudioClip next, float t, bool loop)
    {
        float startVol = musicSource.volume;
        float time = 0f;

        // Fade out current
        while (time < t)
        {
            time += Time.unscaledDeltaTime;
            musicSource.volume = Mathf.Lerp(startVol, 0f, time / t);
            yield return null;
        }

        musicSource.Stop();
        musicSource.clip = next;
        musicSource.loop = loop;
        musicSource.Play();

        // Fade in
        time = 0f;
        while (time < t)
        {
            time += Time.unscaledDeltaTime;
            musicSource.volume = Mathf.Lerp(0f, startVol, time / t);
            yield return null;
        }

        musicSource.volume = startVol;
    }

    public void PlayMusicByIndex(int index, bool loop = true)
    {
        if (musicTracks == null || index < 0 || index >= musicTracks.Length) return;
        PlayMusic(musicTracks[index], loop);
    }

    public void StopMusic() => musicSource.Stop();
    public void PauseMusic() => musicSource.Pause();
    public void ResumeMusic() => musicSource.UnPause();

    // -------- SFX --------
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    // -------- Volumes (hooked to mixer) --------
    public void SetMusicVolume(float volume01)
    {
        float v = Mathf.Clamp(volume01, 0.0001f, 1f);
        audioMixer.SetFloat("MusicVol1", Mathf.Log10(v) * 20f);
    }

    public void SetSFXVolume(float volume01)
    {
        float v = Mathf.Clamp(volume01, 0.0001f, 1f);
        audioMixer.SetFloat("SFXVol1", Mathf.Log10(v) * 20f);
    }
}
