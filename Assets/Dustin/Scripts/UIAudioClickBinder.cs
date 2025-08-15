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

    // Track to avoid double-binding if BindAll runs twice
    private readonly HashSet<int> boundButtons = new HashSet<int>();
    private readonly HashSet<int> boundToggles = new HashSet<int>();
    private readonly HashSet<int> boundDropdowns = new HashSet<int>();
    private readonly HashSet<int> boundSliders = new HashSet<int>();

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
                    int id = b.GetInstanceID();
                    if (boundButtons.Add(id))
                    {
                        b.onClick.AddListener(Play);
                        bCount++;
                    }
                }
            }

            if (includeToggles)
            {
                var list = new List<Toggle>();
                canvas.gameObject.GetComponentsInChildren(true, list);
                foreach (var t in list)
                {
                    int id = t.GetInstanceID();
                    if (boundToggles.Add(id))
                    {
                        t.onValueChanged.AddListener(_ => Play());
                        tCount++;
                    }
                }
            }

            if (includeDropdowns)
            {
                var list = new List<TMP_Dropdown>();
                canvas.gameObject.GetComponentsInChildren(true, list);
                foreach (var d in list)
                {
                    int id = d.GetInstanceID();
                    if (boundDropdowns.Add(id))
                    {
                        d.onValueChanged.AddListener(_ => Play());
                        dCount++;
                    }
                }
            }

            if (includeSliders)
            {
                var list = new List<Slider>();
                canvas.gameObject.GetComponentsInChildren(true, list);
                foreach (var s in list)
                {
                    int id = s.GetInstanceID();
                    if (boundSliders.Add(id))
                    {
                        s.onValueChanged.AddListener(_ => Play());
                        sCount++;
                    }
                }
            }
        }

        if (debugLogs)
            Debug.Log($"[UI_AudioClick_Manager] Bound: Buttons={bCount}, Toggles={tCount}, Dropdowns={dCount}, Sliders={sCount}");
    }

    void Play()
    {
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
