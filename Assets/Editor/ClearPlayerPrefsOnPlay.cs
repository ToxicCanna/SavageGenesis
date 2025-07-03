#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class ClearPlayerPrefsOnPlay
{
    static ClearPlayerPrefsOnPlay()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs cleared on play mode start.");
        }
        else if (state == PlayModeStateChange.ExitingPlayMode)
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs cleared before exiting play mode.");
        }
    }
}
#endif
