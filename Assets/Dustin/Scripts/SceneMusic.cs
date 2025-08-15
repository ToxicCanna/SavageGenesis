using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    [Tooltip("Music to play when this scene loads")]
    public AudioClip music;
    public bool loop = true;
    public bool crossfade = false;
    public float crossfadeTime = 0.75f;

    private void Start()
    {
        if (AudioManager.Instance == null)
        {
            return;
        }
        
        if (music == null)
        {
            return;
        }

        if (crossfade)
        {
            AudioManager.Instance.CrossfadeTo(music, crossfadeTime, loop);
        }
        else
        {
            AudioManager.Instance.PlayMusicIfChanged(music, loop);
        }
    }
}
