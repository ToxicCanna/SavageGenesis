using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_AudioClick_Manager : MonoBehaviour
{
    [Header("Optional per-scene override (else uses AudioManager.uiClickClip)")]
    public AudioClip clickSoundOverride;

    [Header("What to bind")]
    public bool includeButtons = true;
    public bool includeToggles = true;
    public bool includeDropdowns = true;
    public bool includeSliders = false; // sliders can be spammy; off by default

    [Header("Debug")]
    public bool debugLogs = false;

    // Debounce guard: prevents multiple plays in the same frame (e.g., stacked events)
    private static int _lastPlayFrame = -1;

    void Start()
    {
        StartCoroutine(BindNowAndNextFrame());
    }

    System.Collections.IEnumerator BindNowAndNextFrame()
    {
        BindAll();
        yield return null;   // wait one frame in case UI instantiated in Start()
        BindAll();
    }

    void BindAll()
    {
        int bCount = 0, tCount = 0, dCount = 0, sCount = 0;

        foreach (var canvas in FindObjectsOfType<Canvas>(true)) // includeInactive = true
        {
            if (includeButtons)
            {
                var list = new List<Button>();
                canvas.gameObject.GetComponentsInChildren(true, list);
                foreach (var b in list)
                {
                    // Ensure exactly one Play listener
                    b.onClick.RemoveListener(Play);
                    b.onClick.AddListener(Play);
                    bCount++;
                }
            }

            if (includeToggles)
            {
                var list = new List<Toggle>();
                canvas.gameObject.GetComponentsInChildren(true, list);
                foreach (var t in list)
                {
                    // Wrap to match RemoveListener signature
                    UnityEngine.Events.UnityAction<bool> cb = _ => Play();
                    t.onValueChanged.RemoveListener(cb);
                    t.onValueChanged.AddListener(cb);
                    tCount++;
                }
            }

            if (includeDropdowns)
            {
                var list = new List<TMP_Dropdown>();
                canvas.gameObject.GetComponentsInChildren(true, list);
                foreach (var d in list)
                {
                    UnityEngine.Events.UnityAction<int> cb = _ => Play();
                    d.onValueChanged.RemoveListener(cb);
                    d.onValueChanged.AddListener(cb);
                    dCount++;
                }
            }

            if (includeSliders)
            {
                var list = new List<Slider>();
                canvas.gameObject.GetComponentsInChildren(true, list);
                foreach (var s in list)
                {
                    UnityEngine.Events.UnityAction<float> cb = _ => Play();
                    s.onValueChanged.RemoveListener(cb);
                    s.onValueChanged.AddListener(cb);
                    sCount++;
                }
            }
        }

        if (debugLogs)
            Debug.Log($"[UI_AudioClick_Manager] Bound: Buttons={bCount}, Toggles={tCount}, Dropdowns={dCount}, Sliders={sCount}");
    }

    void Play()
    {
        // Debounce: if multiple listeners fire on the same frame, only play once
        if (Time.frameCount == _lastPlayFrame) return;
        _lastPlayFrame = Time.frameCount;

        // Pick clip: per-scene override > AudioManager default
        AudioClip clip = clickSoundOverride;
        var am = AudioManager.Instance;
        if (clip == null && am != null) clip = am.uiClickClip;

        if (clip == null)
        {
            if (debugLogs)
                Debug.LogWarning("[UI_AudioClick_Manager] No click clip (override empty and AudioManager.uiClickClip not assigned).");
            return;
        }

        if (am != null)
        {
            // Goes through SFX mixer group -> SFX slider affects it
            am.PlaySFX(clip);
        }
        else
        {
            // Fallback local play so it still works in isolated test scenes
            var src = GetComponent<AudioSource>();
            if (src == null) src = gameObject.AddComponent<AudioSource>();
            src.playOnAwake = false;
            src.spatialBlend = 0f;
            src.PlayOneShot(clip);
        }

        if (debugLogs) Debug.Log("[UI_AudioClick_Manager] Played UI click.");
    }
}
