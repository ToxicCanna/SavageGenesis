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
        if (playDefaultOnStart && defaultMusic != null)
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
        audioMixer.SetFloat("MusicVol", Mathf.Log10(v) * 20f);
    }

    public void SetSFXVolume(float volume01)
    {
        float v = Mathf.Clamp(volume01, 0.0001f, 1f);
        audioMixer.SetFloat("SFXVol", Mathf.Log10(v) * 20f);
    }
}
